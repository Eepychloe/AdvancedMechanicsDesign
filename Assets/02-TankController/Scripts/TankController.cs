using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankController : MonoBehaviour
{
	private AM_02Tank m_ActionMap; //input
	private Rigidbody m_rb;
	private float m_accel;
	[SerializeField] private float m_tankSpeed;

	[SerializeField] private GameObject m_Turret;
	[SerializeField] private GameObject m_Barrel;
	
	//Track
	[SerializeField] private Track m_rightTrack;
	[SerializeField] private Track m_leftTrack;
	
	//Camera
	private Vector2 m_camAngles;
	[SerializeField] private float m_minXAngleDeg = 10;
	[SerializeField] private float m_maxXAngleDeg = 60;
	[SerializeField] private Transform m_springArm;
	[SerializeField] private Camera m_camera;

	[SerializeField] private float m_biasAngle;

	private TankAmmo m_tankAmmo;
	
	private void Awake()
	{
		m_ActionMap = new AM_02Tank();
		m_rb = GetComponent<Rigidbody>();
		m_tankAmmo = GetComponent<TankAmmo>();
	}

	private void OnEnable()
	{
		m_ActionMap.Enable();

		m_ActionMap.Default.Accelerate.performed += Handle_AcceleratePerformed;
		m_ActionMap.Default.Accelerate.canceled += Handle_AccelerateCanceled;
		m_ActionMap.Default.Steer.performed += Handle_SteerPerformed;
		m_ActionMap.Default.Steer.canceled += Handle_SteerCanceled;
		m_ActionMap.Default.Fire.performed += Handle_FirePerformed;
		m_ActionMap.Default.Fire.canceled += Handle_FireCanceled;
		m_ActionMap.Default.Aim.performed += Handle_AimPerformed;
		m_ActionMap.Default.Zoom.performed += Handle_ZoomPerformed;
	}
	private void OnDisable()
	{
		m_ActionMap.Disable();

		m_ActionMap.Default.Accelerate.performed -= Handle_AcceleratePerformed;
		m_ActionMap.Default.Accelerate.canceled -= Handle_AccelerateCanceled;
		m_ActionMap.Default.Steer.performed -= Handle_SteerPerformed;
		m_ActionMap.Default.Steer.canceled -= Handle_SteerCanceled;
		m_ActionMap.Default.Fire.performed -= Handle_FirePerformed;
		m_ActionMap.Default.Fire.canceled -= Handle_FireCanceled;
		m_ActionMap.Default.Aim.performed -= Handle_AimPerformed;
		m_ActionMap.Default.Zoom.performed -= Handle_ZoomPerformed;
	}

	private void FixedUpdate()
	{
		//Find forward facing direction of the tank body
		Vector3 tankForward = transform.forward;
		
		//all tracks
		Track[] tracks = { m_rightTrack, m_leftTrack };

		foreach (var track in tracks)
		{
			//get all arms
			var suspensionArms = track.GetSuspensionArms();

			foreach (SuspensionArm arm in suspensionArms)
			{
				if (arm.IsGrounded)
				{
					Transform wheel = arm.GetWheel();
					m_rb.AddForceAtPosition(tankForward * (m_accel * m_tankSpeed), wheel.position,
						ForceMode.Acceleration);
				}
			}
		}
	}

	private void Handle_AcceleratePerformed(InputAction.CallbackContext context)
	{
		m_accel = context.ReadValue<float>();
	}

	private void Handle_AccelerateCanceled(InputAction.CallbackContext context)
	{
		m_accel = 0;
	}

	private void Handle_SteerPerformed(InputAction.CallbackContext context)
	{
		Debug.Log("Steer Performed");
	}

	private void Handle_SteerCanceled(InputAction.CallbackContext context)
	{

	}

	private void Handle_FirePerformed(InputAction.CallbackContext context)
	{
		Debug.Log("Fire Performed");
		m_tankAmmo.Fire();
	}

	private void Handle_FireCanceled(InputAction.CallbackContext context)
	{

	}

	private void Handle_AimPerformed(InputAction.CallbackContext context)
	{
		Vector2 deltaPos = context.ReadValue<Vector2>();
		Vector3 camForward = Camera.main.transform.forward;
		
		//flip y delta
		deltaPos.y *= -1;
		
		//X clamped, y not, as would defeat point in camera.
		m_camAngles.x = Mathf.Clamp(m_camAngles.x + deltaPos.y, m_minXAngleDeg, m_maxXAngleDeg);
		m_camAngles.y = Mathf.Repeat(m_camAngles.y + deltaPos.x, 360f);
		
		//Set rotation of spring arm
		m_springArm.rotation = Quaternion.Euler(m_camAngles);
		
		//Turret Rotation
		Quaternion targetRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(camForward, m_Turret.transform.up));
		m_Turret.transform.rotation = targetRotation;
		
		//Barrel Rotation
		Vector3 turretBRight = m_Turret.transform.right;
		Vector3 projVec = Vector3.ProjectOnPlane(camForward, turretBRight);

		float angleDiff = Vector3.SignedAngle(projVec, m_Turret.transform.forward, turretBRight);

		angleDiff = Mathf.Clamp(angleDiff - m_biasAngle, -10, 30);
		Quaternion targetRot = Quaternion.Euler(-angleDiff , 0, 0);
		
		m_Barrel.transform.localRotation = targetRot;
	}

	private void Handle_ZoomPerformed(InputAction.CallbackContext context)
	{
		m_camera.fieldOfView += context.ReadValue<float>() * 2;
	}
}
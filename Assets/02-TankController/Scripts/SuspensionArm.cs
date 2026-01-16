using UnityEngine;

public class SuspensionArm : MonoBehaviour
{
    [SerializeField] private float m_suspensionArmLength = 0.5f;
    [SerializeField] private LayerMask m_suspensionLayerMask;
    [SerializeField] private float m_stiffness;
    [SerializeField] private float m_damping;
    private Vector3 m_restPos;
    private Transform m_wheel;
    private Rigidbody m_rb;

    private bool m_isGrounded;

    public bool IsGrounded
    {
        get => m_isGrounded;
    }

    private float m_raycastHitDist;

    private void Awake()
    {
        m_rb = transform.root.GetComponent<Rigidbody>();
        m_wheel = GetWheel();
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        if (IsGrounded)
            Gizmos.color = Color.green;
        else
        {
            Gizmos.color = Color.red;
        }

        //Draw line from origin to end
        Vector3 endPoint = transform.position + -transform.up * m_raycastHitDist;
        Gizmos.DrawLine(transform.position, endPoint);
        
        //Draw black line for remainder
        Gizmos.color = Color.black;
        Gizmos.DrawLine(endPoint, endPoint + -transform.up * (m_suspensionArmLength - m_raycastHitDist));
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        Vector3 dir = -transform.up;
        
        RaycastHit hitInfo;
        bool hasHit = Physics.Raycast(pos, dir, out hitInfo, m_suspensionArmLength);

        float dist = (!hasHit) ? (m_suspensionArmLength) : (hitInfo.distance);

        m_wheel.position = pos + dir * dist;
        if (!hasHit)
            return;

        float percentOfSpringLength = Mathf.Clamp01(dist / m_suspensionArmLength);
        float compressPercent = 1 - percentOfSpringLength;
        
        float displacementInUnits = compressPercent * m_suspensionArmLength;
        Vector3 vel = m_rb.GetPointVelocity(m_wheel.transform.position);
        float suspensionVel = Vector3.Dot(-m_wheel.transform.up, vel);
        float x = displacementInUnits;
        float k = m_stiffness;
        float force = (-k * x) - m_damping * suspensionVel;
        Vector3 SuspensionForce = -m_wheel.transform.up * force;
        m_rb.AddForceAtPosition(SuspensionForce, m_wheel.position, ForceMode.Acceleration);
        Debug.Log(SuspensionForce);
        
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, m_suspensionArmLength,
                m_suspensionLayerMask))
        {
            //Hit something = grounded
            m_raycastHitDist = hit.distance;
            m_isGrounded = true;
        }
        else
        {
            m_raycastHitDist = 0;
            m_isGrounded = false;
        }
    }

    public Transform GetWheel()
    {
        return transform.GetChild(0);
    }
}

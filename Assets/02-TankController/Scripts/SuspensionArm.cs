using UnityEngine;

public class SuspensionArm : MonoBehaviour
{
    [SerializeField] private float m_suspensionArmLength = 0.5f;
    [SerializeField] private LayerMask m_suspensionLayerMask;

    private bool m_isGrounded;

    public bool IsGrounded
    {
        get => m_isGrounded;
    }

    private float m_raycastHitDist;

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

using UnityEngine;

public class QuaternionVec3MultTest : MonoBehaviour
{
    [SerializeField] private Vector3 m_globalVec;
    [SerializeField] private Vector3 m_rotateVec;
    private void OnDrawGizmos()
    {
        Vector3 localX = transform.rotation * Vector3.right;
        Vector3 localY = transform.rotation * Vector3.up;
        Vector3 localZ = transform.rotation * Vector3.forward;

        // Gizmos.color = Color.red;
        // Gizmos.DrawLine(transform.position, transform.position + localX * 100.0f);
        //
        // Gizmos.color = Color.yellow;
        // Gizmos.DrawLine(transform.position, transform.position + localY * 100.0f);
        //
        // Gizmos.color = Color.blue;
        // Gizmos.DrawLine(transform.position, transform.position + localZ * 100.0f);

        Vector3 TestVec = Quaternion.Euler(m_rotateVec) * m_rotateVec;
        Gizmos.color = Color.purple;
        Gizmos.DrawLine(transform.position, transform.position + m_globalVec * 100.0f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + m_rotateVec * 100.0f);
    }
}

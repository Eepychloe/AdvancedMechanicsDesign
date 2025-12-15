using UnityEditor;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private Vector3 m_lookVector = Vector3.forward;
    [SerializeField] private Vector3 m_upVector = Vector3.up;
    [SerializeField] private Transform m_otherTransform;
    [SerializeField] private float m_angleDegrees;

    private void OnDrawGizmos()
    {
        m_lookVector = (transform.position - m_otherTransform.position.normalized);

        m_upVector = Vector3.zero;
        m_upVector.x = Mathf.Sin(Mathf.Deg2Rad * m_angleDegrees);
        m_upVector.y = Mathf.Cos(Mathf.Deg2Rad * m_angleDegrees);
        m_upVector = Quaternion.LookRotation(m_lookVector, Vector3.up) * m_upVector;
        
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + m_upVector * 100f);
        Handles.Label(transform.position + m_upVector * 2, $"Up: {m_upVector}");

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + m_lookVector * 100f);
        Handles.Label(transform.position + m_lookVector * 2, $"Look: {m_lookVector}");

        transform.rotation = Quaternion.LookRotation(m_lookVector, m_upVector);
    }
}

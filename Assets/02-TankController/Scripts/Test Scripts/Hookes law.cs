using UnityEngine;

public class Hookeslaw : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float m_stiffness;
    [SerializeField] private float m_damping;
    private Vector3 m_restPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        m_restPos = transform.position;
    }

    void FixedUpdate()
    {
        Debug.DrawLine(transform.position, m_restPos);
        Vector3 displacement = transform.position - m_restPos;
        float displacementDown = Vector3.Dot(displacement, -transform.up);
        float velocityDown = Vector3.Dot(rb.linearVelocity, -transform.up);
        float forceMag = (-m_stiffness * displacementDown) - (m_damping * velocityDown);
        Vector3 force = -transform.up * forceMag;
        rb.AddForce(force, ForceMode.Acceleration);

        //Lock axis other than x
        Vector3 localPos = transform.localPosition;
        localPos.Scale(Vector3.right);
        transform.localPosition = localPos;
    }
}

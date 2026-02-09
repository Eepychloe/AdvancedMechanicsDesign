using System.Threading;
using Codice.Client.Common.EventTracking;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] private GameObject firePoint;
    
    public float power = 5f;

    private Rigidbody rb;
    private LineRenderer lineRenderer;

    private Vector3 dragStartPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        Plot(rb, firePoint.transform.position, dragStartPos * power, 500);
        Debug.DrawLine(firePoint.transform.position, dragStartPos * power, Color.red);
    }

    public Vector3[] Plot(Rigidbody rb, Vector3 pos, Vector3 velocity, int steps)
    {
        Vector3[] results = new Vector3[steps];

        float timestep = Time.fixedDeltaTime / Physics.defaultSolverVelocityIterations;
        Vector3 gravityAccel = Physics.gravity * timestep * timestep;

        float drag = 1f - timestep * rb.linearDamping;
        Vector3 moveStep = velocity * timestep;

        for (int i = 0; i < steps; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }
        return results;
    }
}

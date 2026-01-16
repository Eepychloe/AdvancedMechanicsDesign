using UnityEngine;

public class RaycastSuspensionTest : MonoBehaviour
{
    private Rigidbody rb;
    private Transform wheel;
    [SerializeField] private float springLength;
    [SerializeField] private float wheelRadius;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        wheel = transform.GetChild(0);
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        Vector3 dir = -transform.up;

        RaycastHit hitInfo;
        bool hasHit = Physics.Raycast(pos, dir, out hitInfo, springLength);

        float dist = (!hasHit) ? (springLength) : (hitInfo.distance);

        wheel.position = pos + dir * dist;

        float percentOfSpringLength = Mathf.Clamp01(dist / springLength);
        float compressPercent = 1 - percentOfSpringLength;
        Debug.Log(compressPercent);
    }
}

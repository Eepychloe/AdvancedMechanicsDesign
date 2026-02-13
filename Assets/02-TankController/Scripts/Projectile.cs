using UnityEngine;

[RequireComponent(typeof(Trajectory))]
public class Projectile : MonoBehaviour
{
    //For testing
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Rigidbody rb;
    private float velocity;
    private int steps;
    private Trajectory trajectory;

    void Start()
    {
        trajectory = GetComponent<Trajectory>();
        rb = GetComponent<Rigidbody>();
    }

    public void SpawnProjectile(GameObject firePoint)
    {
        
        Instantiate(projectilePrefab, firePoint.transform.position, firePoint.transform.rotation);
        //Vector3 pos = trajectory.Plot(rb, firePoint.transform.position, velocity, steps);
    }


  
}

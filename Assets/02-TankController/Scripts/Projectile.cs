using UnityEngine;

[RequireComponent(typeof(Trajectory))]
public class Projectile : MonoBehaviour
{
    //For testing
    [SerializeField] private GameObject projectilePrefab;
    private Trajectory trajectory;

    void Start()
    {
        trajectory = GetComponent<Trajectory>();
    }

    public void SpawnProjectile(GameObject firePoint)
    {
        
        Instantiate(projectilePrefab, firePoint.transform.position, firePoint.transform.rotation);
        vector3 pos = trajectory.Plot(firePoint.transform.position);
    }


  
}

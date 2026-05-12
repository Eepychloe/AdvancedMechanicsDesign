using UnityEngine;

public class Projectile : MonoBehaviour
{
    //For testing
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float velocity = 20f;
    private int steps;
    private Trajectory trajectory;

    void Start()
    {
        //trajectory = GetComponent<Trajectory>();
    }

    public void SpawnProjectile(GameObject firePoint)
    {
        GameObject bullet = Instantiate(projectilePrefab, firePoint.transform.position, firePoint.transform.rotation);
        Rigidbody bulrb = bullet.GetComponent<Rigidbody>();
        bulrb.linearVelocity = bullet.transform.forward * velocity;
        Debug.Log(transform.forward * velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(projectilePrefab);
    }
}

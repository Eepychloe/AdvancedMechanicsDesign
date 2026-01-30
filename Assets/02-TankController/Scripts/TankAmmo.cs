using UnityEditor;
using UnityEngine;

public class TankAmmo : MonoBehaviour
{
    [SerializeField, HideInInspector] private int  m_currentAmmoCount;
    [SerializeField] private int m_clipSize;

    void Awake()
    {
        m_currentAmmoCount = m_clipSize;
    }
    
    public void Fire()
    {
        if (m_currentAmmoCount > 0)
        {
            //Spawn Projectile etc
        }
        else
        {
            Reload();
        }
        
        m_currentAmmoCount -= 1;
    }

    private void Reload()
    {
        
    }
}

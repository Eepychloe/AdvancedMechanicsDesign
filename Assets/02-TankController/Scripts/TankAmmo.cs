using System.Collections;
using UnityEditor;
using UnityEngine;

public class TankAmmo : MonoBehaviour
{
    [SerializeField, HideInInspector] private int  m_currentAmmoCount;
    [SerializeField] private int m_clipSize;
    private bool m_isReloading = false;
    [SerializeField] private GameObject m_FirePoint; //Pass into projectile spawn function
    private Projectile m_projectile;
    
    void Awake()
    {
        m_currentAmmoCount = m_clipSize;
        m_projectile = gameObject.GetComponent<Projectile>();
    }
    
    private void start()
    {
    }
    
    public void Fire()
    {
        if (m_currentAmmoCount > 0)
        {
            m_projectile.SpawnProjectile(m_FirePoint);
        }
        else
        {
            if (m_isReloading)
            {
                return;
            }

            StartCoroutine(Reload());
            return;
        }
        
        m_currentAmmoCount -= 1;
    }

    IEnumerator Reload()
    {
        m_isReloading = true;
        yield return new WaitForSeconds(2.0f);
        
        m_currentAmmoCount = m_clipSize;
        m_isReloading = false;
        StopCoroutine("Reload");
    }
    
    public int GetCurrentAmmo()
    {
        return m_currentAmmoCount;
    }
    public int GetClipSize()
    {
        return m_clipSize;
    }
}

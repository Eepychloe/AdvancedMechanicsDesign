using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Track : MonoBehaviour
{
    private List<SuspensionArm> m_suspensionArms;
    
    //Traction percentage
    private float m_tractionPercent = 0.0f;
    
    public float TractionPercent {get => m_tractionPercent;}

    private void Awake()
    {
        //get all suspension under the object
        m_suspensionArms = GetComponentsInChildren<SuspensionArm>().ToList();
    }

    private void Update()
    {
        int tractionCounter = 0;

        foreach (SuspensionArm arm in m_suspensionArms)
            tractionCounter += arm.IsGrounded ? 1 : 0;

        if (tractionCounter <= 0)
            m_tractionPercent = 0.0f;

        else
            m_tractionPercent = Mathf.Clamp01(tractionCounter / (float)m_suspensionArms.Count);
    }

    public List<SuspensionArm> GetSuspensionArms()
    {
        return m_suspensionArms;
    }
}

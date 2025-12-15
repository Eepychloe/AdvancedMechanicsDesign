using System;
using UnityEditor;
using UnityEngine;

public class QuaternionMultiplyTest : MonoBehaviour
{
    [SerializeField] private Vector3 m_rot1 = new Vector3(45, 0, 0);
    [SerializeField] private Vector3 m_rot2 = new Vector3(0, 45, 0);

    private void OnDrawGizmos()
    {
        Quaternion rot1 = Quaternion.Euler(m_rot1);
        Quaternion rot2 = Quaternion.Euler(m_rot2);

        Handles.Label(transform.position + Vector3.up * 1.25f, $"rot1: {m_rot1}\nrot2: {m_rot2}");

        transform.rotation = rot1 * rot2;
    }
}

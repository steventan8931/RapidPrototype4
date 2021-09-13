using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Quaternion m_StartRotation;
    public float m_Rotation = -80;
    public float m_RotationSpeed = 2.0f;

    private void Start()
    {
        m_Rotation = -80;
    }
    public void Update()
    {
        m_Rotation -= m_RotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0.0f, m_Rotation, 0.0f);
    }
}

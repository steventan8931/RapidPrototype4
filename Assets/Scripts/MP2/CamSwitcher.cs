using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitcher : MonoBehaviour
{
    public GameObject m_CameraFP;
    public GameObject m_CameraTD;

    public bool m_IsFirstPerson = true;

    private Transform m_PlayerRotation;
    private float cacheRotationY;
    private bool cacheFirstPerson = false;

    private void Start()
    {
        m_CameraFP.SetActive(true);
        m_CameraTD.SetActive(false);
        m_PlayerRotation = FindObjectOfType<FPCharacterMotor>().transform;
        cacheRotationY = m_PlayerRotation.rotation.eulerAngles.y;
    }
    private void Update()
    {
        if (cacheFirstPerson == m_IsFirstPerson)
        {
            if (m_IsFirstPerson)
            {
                cacheRotationY = m_PlayerRotation.rotation.eulerAngles.y;
            }
        }

        cacheFirstPerson = m_IsFirstPerson;

        if (Input.GetKeyDown(KeyCode.B))
        {
            m_IsFirstPerson = !m_IsFirstPerson;
            UpdateCamera();
        }
    }

    private void UpdateCamera()
    {       
        if (m_IsFirstPerson)
        {
            m_PlayerRotation.rotation = Quaternion.Euler(0.0f, cacheRotationY, 0.0f);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            m_CameraFP.SetActive(true);
            m_CameraTD.SetActive(false);
        }
        else
        {
            //Reset Player Rotation due to camera movements
            m_PlayerRotation.rotation = Quaternion.Euler(Vector3.zero);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            m_CameraFP.SetActive(false);
            m_CameraTD.SetActive(true);
        }
    }
}

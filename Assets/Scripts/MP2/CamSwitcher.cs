using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitcher : MonoBehaviour
{
    public GameObject m_CameraFP;
    public GameObject m_CameraTD;

    public bool m_IsFirstPerson = true;

    private void Start()
    {
        m_CameraFP.SetActive(true);
        m_CameraTD.SetActive(false);
    }
    private void Update()
    {
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
            Cursor.lockState = CursorLockMode.Locked;
            m_CameraFP.SetActive(true);
            m_CameraTD.SetActive(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            m_CameraFP.SetActive(false);
            m_CameraTD.SetActive(true);
        }
    }
}

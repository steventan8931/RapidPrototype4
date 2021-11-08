using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private FPCharacterMotor m_Motor;
    public Transform[] m_TeleportLocations;

    private void Start()
    {
        m_Motor = FindObjectOfType<FPCharacterMotor>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Teleport(m_TeleportLocations[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Teleport(m_TeleportLocations[1]);
        }

    }

    public void Teleport(Transform _TeleportEnd)
    {
        m_Motor.m_Controller.enabled = false;
        transform.position = _TeleportEnd.position;
        m_Motor.m_Controller.enabled = true;
    }
}

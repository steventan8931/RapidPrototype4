using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictControl : MonoBehaviour
{
   public ShootingScr m_Shooter;
   public FPCharacterMotor m_Motor;
   public Teleporter m_Teleporter;
   public CamSwitcher m_Cam;
    public FpCameraScr m_FPCam;

    private void Awake()
    {
        m_Shooter = FindObjectOfType<ShootingScr>();
        m_Motor = FindObjectOfType<FPCharacterMotor>();
        m_Teleporter = FindObjectOfType<Teleporter>();
        m_Cam = FindObjectOfType<CamSwitcher>();
    }

    private void Update()
    {
        if (!m_FPCam)
        {
            m_FPCam = FindObjectOfType<FpCameraScr>();
        }
    }

    public void DisableControls()
    {
        m_Shooter.enabled = false;
        m_Motor.enabled = false;
        m_Teleporter.enabled = false;
        m_Cam.enabled = false;
        m_FPCam.enabled = false;
    }
}

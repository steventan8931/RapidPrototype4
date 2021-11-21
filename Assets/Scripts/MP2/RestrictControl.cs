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
    public BuildPlacement m_Build;
    public TopDownCameraController m_TopCam;

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

        if (!m_Build)
        {
            m_Build = FindObjectOfType<BuildPlacement>();
        }

        if (!m_TopCam)
        {
            m_TopCam = FindObjectOfType<TopDownCameraController>();
        }
    }

    public void DisableControls()
    {
        m_Shooter.enabled = false;
        m_Motor.enabled = false;
        m_Teleporter.enabled = false;
        m_Cam.enabled = false;
        m_FPCam.enabled = false;
        m_Build.enabled = false;
        m_TopCam.enabled = false;
    }
}

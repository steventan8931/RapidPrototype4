using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraController : MonoBehaviour
{
    public float m_PanSpeed = 20.0f;
    public float m_PanBorderThickness = 10.0f;
    public Vector2 m_PanLimitXExtents = new Vector2(-10.0f, 10.0f);
    public Vector2 m_PanLimitZExtents = new Vector2(10.0f, 20.0f);

    public float m_MouseScrollSpeed = 20.0f;
    public Vector2 m_ScrollLimitYExtents = new Vector2(10.0f, 40.0f);

    public Transform m_PlayerTransform;
    //private CamSwitcher m_CamSwitcher;

    private void Awake()
    {
        m_PlayerTransform = FindObjectOfType<ShootingScr>().transform;
    }
    private void Start()
    {
        //m_PlayerRotation = transform.parent.transform;
    }

    private void Update()
    {
        Vector3 cameraPos = transform.localPosition;

        //Move Camera with Mouse/Keyboard
        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - m_PanBorderThickness)
        {
            cameraPos.z += m_PanSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= m_PanBorderThickness)
        {
            cameraPos.z -= m_PanSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - m_PanBorderThickness)
        {
            cameraPos.x += m_PanSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <=  m_PanBorderThickness)
        {
            cameraPos.x -= m_PanSpeed * Time.deltaTime;
        }



        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        cameraPos.y -= scrollWheel * 100.0f * m_MouseScrollSpeed * Time.deltaTime;

        //Limit the values
        cameraPos.x = Mathf.Clamp(cameraPos.x, m_PanLimitXExtents.x, m_PanLimitXExtents.y);
        cameraPos.y = Mathf.Clamp(cameraPos.y, m_ScrollLimitYExtents.x, m_ScrollLimitYExtents.y);
        cameraPos.z = Mathf.Clamp(cameraPos.z, m_PanLimitZExtents.x, m_PanLimitZExtents.y);

        transform.localPosition = cameraPos;

        if (Input.GetKey(KeyCode.Space))
        {
            transform.position = new Vector3(m_PlayerTransform.position.x, transform.position.y, m_PlayerTransform.position.z);
        }
    }
}

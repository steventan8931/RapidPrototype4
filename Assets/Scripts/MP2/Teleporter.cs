using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private FPCharacterMotor m_Motor;

    public GameObject m_BuildablePrefab;
    public GameObject m_ShadowPrefab;
    
    public float m_MaxDistance = 500.0f;

    public GameObject m_CreateParticles;
    public float m_CreateTimer = 0.0f;
    public float m_CreateDelay = 0.8f;
    public bool m_Created = false;
    private void Start()
    {
        m_Motor = FindObjectOfType<FPCharacterMotor>();
    }

    private void Update()
    {
        if (m_Created)
        {
            m_CreateTimer += Time.deltaTime;

            if (m_CreateTimer > m_CreateDelay)
            {
                if (!m_ShadowPrefab)
                {
                    m_ShadowPrefab = Instantiate(m_BuildablePrefab, Camera.main.transform);
                    m_Created = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!m_ShadowPrefab)
            {
                Instantiate(m_CreateParticles, Camera.main.transform);
                m_Created = true;
                m_Motor.m_Animation.SetBool("CastTeleport", true);
            }
        }

        if (m_ShadowPrefab)
        {
            MoveObjectToMouse();
        }
    }

    public void Teleport(Transform _TeleportEnd)
    {
        m_Motor.m_Controller.enabled = false;
        Instantiate(m_CreateParticles, Camera.main.transform);
        transform.position = _TeleportEnd.position;
        m_Motor.m_Controller.enabled = true;
    }

    private void MoveObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo,m_MaxDistance))
        {
            m_ShadowPrefab.transform.position = hitInfo.point;
            m_ShadowPrefab.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }

        //If object has a collider
        if (hitInfo.collider != null)
        {
            //if (!hitInfo.collider.gameObject.CompareTag("Floor") && !hitInfo.collider.gameObject.CompareTag("Enemy"))
            if (hitInfo.collider.gameObject.CompareTag("Placeable"))
            {
                //m_CurrentPlaceableObject.transform.GetChild(0).GetComponent<Renderer>().material = m_CanPlaceMat;
                m_ShadowPrefab.GetComponent<TeleportShadow>().CheckValid(true);

                BoxCollider PlaceableCollider = m_ShadowPrefab.gameObject.GetComponent<BoxCollider>();
                PlaceableCollider.isTrigger = true;
                Vector3 BoxCenter = m_ShadowPrefab.gameObject.transform.position + PlaceableCollider.center;
                Vector3 HalfExtents = PlaceableCollider.size / 2;

                if (Physics.CheckBox(BoxCenter, HalfExtents, Quaternion.identity))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        //PlaceableCollider.isTrigger = false;
                        //m_ShadowPrefab.layer = 30;
                        Teleport(m_ShadowPrefab.transform);
                        Destroy(m_ShadowPrefab);
                    }
                }
            }
            else
            {
                m_ShadowPrefab.GetComponent<TeleportShadow>().CheckValid(false);
            }
        }
        else
        {
            m_ShadowPrefab.GetComponent<TeleportShadow>().CheckValid(false);
        }
    }
}

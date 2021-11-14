using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private FPCharacterMotor m_Motor;
    public Transform[] m_TeleportLocations;


    public GameObject m_ShadowPrefab;

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

    private void MoveObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
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
                m_ShadowPrefab.GetComponent<PlaceableObject>().CheckValid(true);

                BoxCollider PlaceableCollider = m_ShadowPrefab.gameObject.GetComponent<BoxCollider>();
                PlaceableCollider.isTrigger = true;
                Vector3 BoxCenter = m_ShadowPrefab.gameObject.transform.position + PlaceableCollider.center;
                Vector3 HalfExtents = PlaceableCollider.size / 2;

                if (Physics.CheckBox(BoxCenter, HalfExtents, Quaternion.identity))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        //PlaceableCollider.isTrigger = false;
                        m_ShadowPrefab.layer = 30;
    
                        m_ShadowPrefab = null;
                    }
                }
            }
            else
            {
                //m_CurrentPlaceableObject.transform.GetChild(0).GetComponent<Renderer>().material = m_CantPlaceMat;
                m_ShadowPrefab.GetComponent<PlaceableObject>().CheckValid(false);
            }
        }
    }
}

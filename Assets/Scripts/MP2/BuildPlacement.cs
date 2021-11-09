using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlacement : MonoBehaviour
{
    public GameObject m_BuildablePrefab;

    private KeyCode newObjectHotKey = KeyCode.A;

    public GameObject m_CurrentPlaceableObject;

    public Material m_CantPlaceMat;
    public Material m_CanPlaceMat;

    private CamSwitcher m_CamSwitcher;
    private NewCrafting m_Crafting;

    private void Start()
    {
        m_CamSwitcher = FindObjectOfType<CamSwitcher>();
        m_Crafting = FindObjectOfType<NewCrafting>();
    }

    private void Update()
    {
        //Only run this script if the player is in top down view
        if (m_CamSwitcher.m_IsFirstPerson)
        {
            Destroy(m_CurrentPlaceableObject);
            return;
        }

        //Right click to remove item
        if (Input.GetMouseButtonDown(1))
        {
            if (m_CurrentPlaceableObject == null)
            {
                //m_CurrentPlaceableObject = Instantiate(m_BuildablePrefab);

            }
            else
            {
                Destroy(m_CurrentPlaceableObject);
            }
        }

        if (m_CurrentPlaceableObject != null)
        {
            MoveObjectToMouse();

        }
    }

    private void MoveObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            m_CurrentPlaceableObject.transform.position = hitInfo.point;
            m_CurrentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }

        //If object has a collider
        if (hitInfo.collider != null)
        {
            if (!hitInfo.collider.gameObject.CompareTag("Floor") && !hitInfo.collider.gameObject.CompareTag("Enemy"))
            {
                //m_CurrentPlaceableObject.transform.GetChild(0).GetComponent<Renderer>().material = m_CanPlaceMat;
                m_CurrentPlaceableObject.GetComponent<PlaceableObject>().CheckValid(true);

                BoxCollider PlaceableCollider = m_CurrentPlaceableObject.gameObject.GetComponent<BoxCollider>();
                PlaceableCollider.isTrigger = true;
                Vector3 BoxCenter = m_CurrentPlaceableObject.gameObject.transform.position + PlaceableCollider.center;
                Vector3 HalfExtents = PlaceableCollider.size / 2;

                if (Physics.CheckBox(BoxCenter, HalfExtents, Quaternion.identity))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        //PlaceableCollider.isTrigger = false;
                        m_CurrentPlaceableObject.layer = 30;
                        m_Crafting.m_Inventory.m_MagicCrystalCount -= m_Crafting.m_Cost;
                        //reenable turret script afte being placed
                        m_CurrentPlaceableObject.GetComponent<TurretScr>().enabled = true;
                        Debug.Log("craft cost" + m_Crafting.m_Cost);
                        m_CurrentPlaceableObject = null;
                    }
                }
            }
            else
            {
                //m_CurrentPlaceableObject.transform.GetChild(0).GetComponent<Renderer>().material = m_CantPlaceMat;
                m_CurrentPlaceableObject.GetComponent<PlaceableObject>().CheckValid(false);
            }
        }


    }
}

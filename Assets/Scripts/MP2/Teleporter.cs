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

    public bool m_Teleporting = false;
    private float m_TeleportTimer = 0.0f;
    public float m_TeleportDelay = 1.0f;
    public Vector3 cacheTransform;

    private MP2AudioManager m_AudioManager;
    private CamSwitcher m_CamSwitch;

    private void Start()
    {
        m_Motor = FindObjectOfType<FPCharacterMotor>();
        m_AudioManager = FindObjectOfType<MP2AudioManager>();
        m_CamSwitch = FindObjectOfType<CamSwitcher>();
    }

    private void Update()
    {
        if (!m_CamSwitch.m_IsFirstPerson)
        {
            return;
        }
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
                //Instantiate(m_CreateParticles, Camera.main.transform);
                m_AudioManager.PlaySound("Teleport");
                m_Created = true;
                m_Motor.m_Animation.SetBool("CastTeleport", true);
            }
            else
            {
                Destroy(m_ShadowPrefab);
            }
        }

        //Right click to remove item
        if (Input.GetMouseButtonDown(0))
        {
            if (m_ShadowPrefab)
            {
                Destroy(m_ShadowPrefab);
            }
        }

        if (m_ShadowPrefab)
        {
            MoveObjectToMouse();
        }

        if (m_Teleporting)
        {
 
            m_TeleportTimer += Time.deltaTime;
            if (m_TeleportTimer > m_TeleportDelay)
            {
                Teleport(cacheTransform);
                Destroy(m_ShadowPrefab);
                m_TeleportTimer = 0.0f;
                m_Teleporting = false;
            }

        }
    }

    public void Teleport(Vector3 _TeleportEnd)
    {
        m_Motor.m_Controller.enabled = false;
        transform.position = _TeleportEnd;
        m_Motor.m_Controller.enabled = true;
    }

    private void MoveObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo,m_MaxDistance))
        {
            //hitInfo.point += new Vector3(0, 10.0f, 0);
            Debug.Log("x = " + m_ShadowPrefab.transform.localRotation.eulerAngles.x);
            Debug.Log("y = " + m_ShadowPrefab.transform.localRotation.eulerAngles.y);
            Debug.Log("z = " + m_ShadowPrefab.transform.localRotation.eulerAngles.z);
            m_ShadowPrefab.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            if (m_ShadowPrefab.transform.localRotation.eulerAngles.y == 0 || m_ShadowPrefab.transform.localRotation.eulerAngles.y == 180)
            {
                hitInfo.point += new Vector3(0, 25.0f, 0);
                m_ShadowPrefab.transform.position = hitInfo.point - hitInfo.normal * 25;
                m_ShadowPrefab.transform.localRotation = Quaternion.Euler(0, m_ShadowPrefab.transform.localRotation.y, 0);
            }
            else
            {

                m_ShadowPrefab.transform.position = hitInfo.point + hitInfo.normal * 5;
            }
        }


            //If object has a collider
        //if (hitInfo.collider != null)
        {
            //if (!hitInfo.collider.gameObject.CompareTag("Floor") && !hitInfo.collider.gameObject.CompareTag("Enemy"))
            if (hitInfo.collider.gameObject.CompareTag("Placeable") || hitInfo.collider.gameObject.CompareTag("Wall"))
            {
                //m_CurrentPlaceableObject.transform.GetChild(0).GetComponent<Renderer>().material = m_CanPlaceMat;
                m_ShadowPrefab.GetComponent<TeleportShadow>().CheckValid(true);

                BoxCollider PlaceableCollider = m_ShadowPrefab.gameObject.GetComponent<BoxCollider>();
                PlaceableCollider.isTrigger = true;
                Vector3 BoxCenter = m_ShadowPrefab.gameObject.transform.position + PlaceableCollider.center;
                Vector3 HalfExtents = PlaceableCollider.size / 2;

                //if (Physics.CheckBox(BoxCenter, HalfExtents, Quaternion.identity))
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        //PlaceableCollider.isTrigger = false;
                        //m_ShadowPrefab.layer = 30;
                        //cacheTransform = m_ShadowPrefab.transform.position;
                        if (!m_Teleporting)
                        {
                            cacheTransform = m_ShadowPrefab.transform.position;
                            m_ShadowPrefab.transform.parent = null;
                            //cacheTransform = hitInfo.point + hitInfo.normal * 5;
                            Instantiate(m_CreateParticles, Camera.main.transform);
                            m_AudioManager.PlaySound("Teleport");
                        }
                        m_Teleporting = true;

                    }
                }

            }
            else
            {
                m_ShadowPrefab.GetComponent<TeleportShadow>().CheckValid(false);
            }
        }
        //else
        {
            //m_ShadowPrefab.GetComponent<TeleportShadow>().CheckValid(false);
        }
    }
}

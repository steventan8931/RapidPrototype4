using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatFollowScr : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform lookat;
    public Vector3 offset;

    private Camera cam;

    public void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = cam.WorldToScreenPoint(lookat.position + offset);
        if(transform.position != pos)
        {
            transform.position = pos;
        }

    }
}

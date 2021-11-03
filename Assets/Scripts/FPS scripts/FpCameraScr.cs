using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpCameraScr : MonoBehaviour
{
    public float sensitivity = 100f;
    public Transform player;

    float rotationUpDown = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //get input from mouse
        float x = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        //rotation up and down
        rotationUpDown -= y;
        rotationUpDown = Mathf.Clamp(rotationUpDown, -90f, 90f);
        transform.localRotation = Quaternion.Euler(rotationUpDown, 0f, 0f);

        //rotation right and left
        player.Rotate(Vector3.up * x);
    }
}

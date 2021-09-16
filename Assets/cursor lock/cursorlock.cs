using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorlock : MonoBehaviour
{
 

    public bool mouseLocked = true;

    private void LockMouse()
    {
        Cursor.lockState = mouseLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !mouseLocked;
    }

    void Awake()
    {
        LockMouse();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            mouseLocked = !mouseLocked;
            LockMouse();   
        }

    }
}

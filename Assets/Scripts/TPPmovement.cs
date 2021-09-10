using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPPmovement : MonoBehaviour
{
  
    public CharacterController controller;
    //var for movement
    public Transform cam;
    public float speed = 5f;
    public float jumpSpd = 4f;
    public float gravity = 3.8f;
    public float tempDirY;
   
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            print("moving state");
            float targetAng = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAng, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 movedir = Quaternion.Euler(0f, targetAng, 0f) * Vector3.forward;
            if(controller.isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    tempDirY = jumpSpd;
                }
            }
            

            tempDirY -= gravity * Time.deltaTime;
            //movedir.y = tempDirY;
            Vector3 tempMoveDir;
            tempMoveDir = movedir.normalized;
            tempMoveDir.y = tempDirY;

            controller.Move(tempMoveDir * speed * Time.deltaTime);
        }
        else if(Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            //print("jumped but not moving");
            Vector3 pureJumpDir = new Vector3(0f, (jumpSpd)*speed, 0f);
            print(pureJumpDir.y);
            controller.Move(pureJumpDir * speed *Time.deltaTime);
        }
        else if(!controller.isGrounded)
        {
            Vector3 fallDir = new Vector3(0f, -gravity*Time.deltaTime, 0f);
            controller.Move(fallDir.normalized *speed*Time.deltaTime);
            print("falling speed:" + fallDir.normalized.y * Time.deltaTime);
        }
        
        
    }
}

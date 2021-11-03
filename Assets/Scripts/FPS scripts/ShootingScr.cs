using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShootingScr : MonoBehaviour
{
    //bullet param
    public GameObject bullet;
    public float shootForce, upwardForce;

    //weapon stat
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    public int bulletsLeft, bulletsShot;

    public bool isShooting, rdyToShoot, reloading;

    public bool allowInvoke;
    public Camera Cam;
    public Transform attackPoint;

    public TextMeshProUGUI ammoDisplay;
    
    
    private void Awake()
    {
        bulletsLeft = magazineSize;
        rdyToShoot = true;
    }

    private void Update()
    {
        shootInput();

        if(ammoDisplay != null)
        {
            ammoDisplay.SetText(bulletsLeft / bulletsPerTap + "/" + magazineSize / bulletsPerTap);
        }
    }

    void shootInput()
    {
        //if allowed to hold down button to shoot
        if(allowButtonHold)
        {
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }else
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }
        
        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading )
        {
            Reload();
        }
        //auto reload when no bullets left
        if(rdyToShoot && isShooting && !reloading && bulletsLeft <=0)
        {
            Reload();
        }
        if(rdyToShoot && isShooting && !reloading && bulletsLeft>0)
        {
            bulletsShot = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        rdyToShoot = false;

        //find hit position using raycast
        Ray ray = Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //check if ray hits anything
        Vector3 targetPoint;
        if(Physics.Raycast(ray,out hit))
        {
            targetPoint = hit.point;
        }else
        {
            targetPoint = ray.GetPoint(80); // not hitting, then get a point that far away  from player
        }

        //calculate direction from attackpoint to target point
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //calculate spread
        float x = UnityEngine.Random.Range(-spread, spread);
        float y = UnityEngine.Random.Range(-spread, spread);

        //new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        //Spawn bullet
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        Debug.Log("fired one bullet!");
        //rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        //add force 
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);


        bulletsLeft--;
        bulletsShot++;

        if(allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }
        // if more than one bulletsPerTap make sure to repeat shoot function
        if(bulletsShot < bulletsPerTap && bulletsLeft >0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    void ResetShot()
    {
        //allow shooting and invoke again
        rdyToShoot = true;
        allowInvoke = true;
    }
     
    void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);     
    }
    void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}

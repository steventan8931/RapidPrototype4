using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShootingScr : MonoBehaviour
{
    //bullet param
    public GameObject bullet,fireBullet,iceBullet,buffBullet;
    public float shootForce, upwardForce;

    //weapon stat
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    public float bulletsLeft, bulletsShot;

    public bool isShooting, rdyToShoot, reloading;
    public int bulletType = 1;

    public bool allowInvoke;
    public Camera Cam;
    public Transform attackPoint;

    public TextMeshProUGUI ammoDisplay;
    public TextMeshProUGUI ammoType;
    public GameObject ReloadReminder;
    public CamSwitcher camswitcher;

    private FPCharacterMotor m_CharacterMotor;

    private void Awake()
    {
        // bulletsLeft = magazineSize;
        bulletsLeft = 5f;
        rdyToShoot = true;
        camswitcher = FindObjectOfType<CamSwitcher>();
        m_CharacterMotor = FindObjectOfType<FPCharacterMotor>();
    }

    private void Update()
    {
        if(!camswitcher.m_IsFirstPerson)
        {
            return;
        }
        switchAmmo();
        shootInput();
        textDraw();
        

    }
    void textDraw()
    {
        if (ammoDisplay != null)
        {
            ammoDisplay.SetText(bulletsLeft / bulletsPerTap + "/" + magazineSize / bulletsPerTap);
        }
        if(ammoType != null)
        {
            if(bulletType == 1)
            {
                ammoType.SetText("Regular Bullet");
            }

            if (bulletType == 2)
            {
                ammoType.SetText("Fire Bullet");
            }

            if (bulletType == 3)
            {
                ammoType.SetText("Ice Bullet");
            }

            if (bulletType == 4)
            {
                ammoType.SetText("Turret Buff");
            }

        }
    }
    void shootInput()
    {
        if(bulletsLeft < magazineSize)
        {
            bulletsLeft += Time.deltaTime * 2;
            if(bulletsLeft > magazineSize)
            {
                bulletsLeft = magazineSize;
            }
        }
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
           // Reload();
        }
        //auto reload when no bullets left
        if(rdyToShoot && isShooting && !reloading && bulletsLeft <=0)
        {
            // Reload();
            return;
        }
        if(rdyToShoot && isShooting && !reloading && bulletsLeft>0)
        {
            bulletsShot = 0;
            Shoot();
        }
    }
    void switchAmmo()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            bulletType = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            bulletType = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            bulletType = 3;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            bulletType = 4;
        }
    }
    private void Shoot()
    {
        rdyToShoot = false;
        m_CharacterMotor.m_Animation.SetBool("Shooting", true);
        m_CharacterMotor.m_Animation.speed = 1.0f;
        //find hit position using raycast
        Ray ray = Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //check if ray hits anything
        Vector3 targetPoint;
        if(Physics.Raycast(ray,out hit))
        {
            targetPoint = hit.point;
            Debug.Log("have a hit target");
        }else
        {
            targetPoint = ray.GetPoint(80); // not hitting, then get a point that far away  from player
            Debug.Log("raycast not hitting");
        }

        //calculate direction from attackpoint to target point
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //calculate spread
        float x = UnityEngine.Random.Range(-spread, spread);
        float y = UnityEngine.Random.Range(-spread, spread);

        //new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        //Spawn bullet
        GameObject currentBullet = null;
        if (bulletType == 1)
        {
           currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        }
        if(bulletType == 2)
        {
           currentBullet = Instantiate(fireBullet, attackPoint.position, Quaternion.identity);
        }
        if (bulletType == 3)
        {
            currentBullet = Instantiate(iceBullet, attackPoint.position, Quaternion.identity);
        }
        if (bulletType == 4)
        {
            currentBullet = Instantiate(buffBullet, attackPoint.position, Quaternion.identity);
        }
        Debug.Log("fired one bullet!");
        //rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        //add force 
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);

        if(bulletType != 1)
        {
            if(bulletType == 2 && bulletsLeft >=2)
            {
                bulletsLeft-=2;
            }
            if(bulletType == 3 && bulletsLeft >=3)
            {
                bulletsLeft-=3;
            }
        }
        
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
        m_CharacterMotor.m_Animation.SetBool("Shooting", false);
        //m_CharacterMotor.m_Animation.speed = 0.0f;
        //allow shooting and invoke again
        rdyToShoot = true;
        allowInvoke = true;
    }
     
    void Reload()
    {
        reloading = true;
        ReloadReminder.SetActive(true);
        Invoke("ReloadFinished", reloadTime);     
    }
    void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
        ReloadReminder.SetActive(false);
    }
}

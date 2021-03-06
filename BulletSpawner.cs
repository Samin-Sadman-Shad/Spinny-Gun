using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    Rigidbody bulletRb;

    public GameObject gun;
    public GameObject target;

    public Vector3 startingPoint;

    void Awake()
    {
        bulletRb = bulletPrefab.GetComponent<Rigidbody>();
        //target = FindObjectOfType<TargetSpawner>().TargetSpawnSingle();
        GameManager.KeyPressed += KeyIsPressed;
        
    }

    public void KeyIsPressed()
    {
        target = FindObjectOfType<PathFollower>().gameObject;
        startingPoint = gun.GetComponent<Gun>().GetPointerPosition(); 
        //Debug.Log("starting point for the bullet is  " + startingPoint);

        int ammo = GameManager.ammoNumber;
        //Debug.Log(ammo);
        if (ammo > 0 && target != null)
        {
             GameObject bullet = Instantiate(bulletPrefab, startingPoint, bulletPrefab.transform.rotation);

             Vector3 relativeTargetDirection = gun.GetComponent<Gun>().GetPointerDirection();
            bullet.GetComponent<BulletBehavior>().ThrowBullet(relativeTargetDirection, startingPoint);

            //bullet.GetComponent<BulletBehavior>().ThrowBullet(target.transform.position, startingPoint);
            GameManager.ammoNumber--;
            GameManager.AmmoText.SetText("Ammo: " + GameManager.ammoNumber);
             //Debug.Log("bullets are" + GameManager.ammoNumber);
        }
    }

    /*
    void LateUpdate()
    {
        startingPoint = gun.GetComponent<Gun>().GetPointerPosition();
        //startingPoint = Gun.GetPointerPosition();
        Debug.Log("starting point for the bullet is  " + startingPoint);

        int ammo = GameManager.ammoNumber;
        //Debug.Log(ammo);
        if (ammo > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject bullet = Instantiate(bulletPrefab, startingPoint, bulletPrefab.transform.rotation);

                bullet.GetComponent<BulletBehavior>().ThrowBullet(target.transform.position, startingPoint);
                GameManager.ammoNumber--;
            }
        }
    }
    */
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float rotationSpeed;

    Rigidbody gunRb;

    Vector3 firstPosition;

    bool IsTorqueAdded;

    public float torque;
    // Start is called before the first frame update
    void Start()
    {
        gunRb = GetComponent<Rigidbody>();
        firstPosition = gameObject.transform.position;
        IsTorqueAdded = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        int ammo = GameManager.ammoNumber;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);

        if (Input.GetMouseButtonDown(0) && ammo > 0)
        {
            if(IsTorqueAdded == false)
            {
                AddTorqueToGun();
                IsTorqueAdded = true;
                transform.position = firstPosition;
                //StartCoroutine(WaitToAddTorqueAgain());
                IsTorqueAdded = false;
            }
            //gunRb.AddTorque(Vector3.forward, ForceMode.Impulse);
            transform.position = firstPosition;

        }
        else
        {
            transform.position = firstPosition;
        }
       // Debug.Log("the position of gun pointer is " + GetPointerPosition());
    }

    public Vector3 GetPointerPosition()
    {
        //Debug.Log(transform.GetChild(0).transform.GetChild(0).gameObject.name);
        Vector3 pointerPosition = transform.GetChild(0).transform.GetChild(0).transform.position;
        return pointerPosition;
    }

    
    public Vector3 GetPointerDirection()
    {
        Vector3 localY;
        localY = transform.GetChild(0).transform.GetChild(1).transform.position;
        return localY;
    }

    void AddTorqueToGun()
    {
        gunRb.AddTorque(Vector3.forward * torque, ForceMode.Impulse);
    }

    IEnumerator WaitToAddTorqueAgain()
    {
        yield return new WaitForSeconds(5);
    }

}

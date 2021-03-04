using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    //Rigidbody bulletRb;
    //public GameObject target;
    //public GameObject gun;
    //public GameObject bulletPrefab;

    public float forceValue;
    public GameObject gameManager;
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            GameManager.score++;
            GameManager.scoreText.SetText("Score: " + GameManager.score);
            //Debug.Log("the score is " + GameManager.score);
        }
        else if(collision.gameObject.CompareTag("Special Target"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            GameManager.gameOver = true;
        }
    }

    public void ThrowBullet(Vector3 targetPosition, Vector3 gunPointerPosition)
    {
        gameObject.SetActive(true);
        Rigidbody bulletRb = gameObject.GetComponent<Rigidbody>();
        //Debug.Log("bullet should move now ");
        //Debug.DrawRay(gunPointerPosition, (targetPosition - gunPointerPosition));
        //Debug.DrawRay(gunPointerPosition, (targetPosition - gunPointerPosition));
        bulletRb.AddForce((targetPosition - gunPointerPosition) * forceValue, ForceMode.Impulse);
        //gameObject.transform.Translate((targetPosition - gunPointerPosition) * Time.deltaTime , Space.World);
    }
}

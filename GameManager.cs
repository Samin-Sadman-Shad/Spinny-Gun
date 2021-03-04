using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int ammoNumberPerLevel = 10;
    
    public static int level;
    public static int ammoNumber;
    public static int score;

    PathFollower[] targets;
    public GameObject gun;
    
    public static event Action KeyPressed;
    public static event Action NewLevel;

    public static TextMeshProUGUI scoreText;
    public static TextMeshProUGUI LevelText;
    public static TextMeshProUGUI AmmoText;
    public static TextMeshProUGUI gameOverText;

    public static bool gameOver;
    TextMeshProUGUI[] allText;

    void Awake()
    {
        level = 0;
        score = 0;
        //scoreText = FindObjectOfType<TextMeshProUGUI>();
        gun = FindObjectOfType<Gun>().gameObject;
        allText = FindObjectsOfType<TextMeshProUGUI>();
        for(int i=0; i < allText.Length; i++)
        {
            if (allText[i].CompareTag("ScoreText"))
            {
                scoreText = allText[i];
            }
            else if (allText[i].CompareTag("Level"))
            {
                LevelText = allText[i];
            }
            else if (allText[i].CompareTag("Ammo"))
            {
                AmmoText = allText[i];
            }
            else if (allText[i].CompareTag("GameOver"))
            {
                gameOverText = allText[i];
            }

        }

        scoreText.SetText("Score: " + score);
        ammoNumber = ammoNumberPerLevel + level;
        GameManager.AmmoText.SetText("Ammo: " + GameManager.ammoNumber);
        NewLevel += ChangeAmmoNumber;
        //Debug.Log("the score is " + score);
    }

    void ChangeAmmoNumber()
    {
        ammoNumber = ammoNumberPerLevel + level;
        GameManager.AmmoText.SetText("Ammo: " + GameManager.ammoNumber);
        //Debug.Log("total bullets are " + ammoNumber);
    }

   
    void Update()
    {
        targets = FindObjectsOfType<PathFollower>();
        if(targets.Length == 0)
        {
            level++;
            NewLevel();
            LevelText.SetText("Level: " + level);
            //Debug.Log("level is " + level);
            //Debug.Log(NewLevel.GetInvocationList().Length);
        }

        if (Input.GetMouseButtonDown(0))
        {
            KeyPressed();
            /*
            Delegate[] keyPressedList = KeyPressed.GetInvocationList();
            for (int i=0; i< KeyPressed.GetInvocationList().Length; i++)
            {
                Debug.Log(keyPressedList[i]);
            }
            */
        }
        //Debug.Log(ammoNumber);
        
        if(ammoNumber < 1 || gameOver)
        {
            //Debug.Log("Game Over");
            PathFollower[] targets = FindObjectsOfType<PathFollower>();
            for(int i=0; i<targets.Length; i++)
            {
                if(targets[i].movingSpeed > 0)
                {
                    targets[i].movingSpeed -= Time.deltaTime * 5;
                }
            }

            if (gun.activeInHierarchy)
            {
                gun.SetActive(false);
            }
            gameOverText.SetText("Game Over");

        }
        
    }
}

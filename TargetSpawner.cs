using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject sampleTarget;
    public static GameObject specialTarget;
    public Node[] path;

    public int currentNumberOfTarget;
    // Start is called before the first frame update
    void Start()
    {
        path = Path.path;
        //Debug.Log(path.Length);
        GameManager.NewLevel += TargetSpawn;
    }


     public GameObject[] Spawn(int n)
     {
        //float xPosition = Random.Range(path[0].transform.position.x, path[3].transform.position.x);
        //float yPosition = Random.Range(path[2].transform.position.y, path[3].transform.position.y);
        GameObject[] targets = new GameObject[n];

        Vector3 offset1 = new Vector3(-0.1f, 0, 0);
        Vector3 offset2 = new Vector3(0, -2, 0);

        Vector3 spawnPos1 = path[7].transform.position + offset1;
        Vector3 spawnPos2 = path[1].transform.position + offset2;
        //Vector3 spawningPos1 = sampleTarget.transform.position

        if(n % 5 != 0)
        {
            Debug.Log(n);
            Debug.Log(n % 5);
            for (int i = 0; i < n; i++)
            {
                if (i == 0)
                {
                    targets[i] = Instantiate(sampleTarget, sampleTarget.transform.position, sampleTarget.transform.rotation);
                    targets[i].GetComponent<PathFollower>().SetCurrentNodeIndex(0);
                }
                else if (i == 1)
                {
                    targets[i] = Instantiate(sampleTarget, spawnPos1, sampleTarget.transform.rotation);
                    targets[i].GetComponent<PathFollower>().SetCurrentNodeIndex(0);
                }
                else if (i == 2)
                {
                    targets[i] = Instantiate(sampleTarget, spawnPos2, sampleTarget.transform.rotation);
                    targets[i].GetComponent<PathFollower>().RotateTargetPrimary();
                    targets[i].GetComponent<PathFollower>().SetCurrentNodeIndex(2);
                }
                else if (i == 3)
                {
                    targets[i] = Instantiate(sampleTarget, spawnPos2 + offset2, sampleTarget.transform.rotation);
                    targets[i].GetComponent<PathFollower>().RotateTargetPrimary();
                    targets[i].GetComponent<PathFollower>().SetCurrentNodeIndex(2);
                }
                else
                {
                    break;
                }

            }
        }
        
        else if (n % 5 == 0)
        {
            Debug.Log(n);
            Debug.Log("special level");
            PathFollower[] targetComponent = FindObjectsOfType<PathFollower>();
            for (int i = 0; i < targetComponent.Length; i++)
            {
                Destroy(targetComponent[i].gameObject);
            }

            specialTarget = Instantiate(sampleTarget, spawnPos1, sampleTarget.transform.rotation);
            specialTarget.tag = "Special Target";
            if (n % 10 == 0)
            {
                //specialTarget = Instantiate(sampleTarget, spawnPos1, sampleTarget.transform.rotation);
                specialTarget.GetComponent<MeshRenderer>().material.color = Color.red;
            }
            else
            {
                specialTarget.GetComponent<MeshRenderer>().material.color = Color.yellow;
            }
        }

        return targets;
     }

    public void TargetSpawn()
    {
         Spawn(GameManager.level);
        Debug.Log("level is " + GameManager.level);
        //TargetSpawnSingle();
    }

    public GameObject TargetSpawnSingle()
    {
       GameObject singleTarget = Instantiate(sampleTarget, sampleTarget.transform.position, sampleTarget.transform.rotation);
        return singleTarget;
    }
}

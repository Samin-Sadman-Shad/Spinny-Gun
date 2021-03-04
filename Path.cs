using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public static Node[] path;
    public static Vector3 currentNodePosition;
    void Awake()
    {
        path = GetComponentsInChildren<Node>();
        //Debug.Log(path.Length);
        /*
        for(int i=0; i<path.Length; i++)
        {
            Debug.Log(path[i].gameObject.name);
        }
        */
    }

    // SetCurrentNodePosition will set the position of the current node
    public static Vector3 SetCurrentNodePosition(int currentNodeIndex)
    {
        //Debug.Log("Index of the current node " + currentNodeIndex);
        currentNodePosition = path[currentNodeIndex].transform.position;
        return currentNodePosition;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PathFollower : MonoBehaviour
{
    //All the Field variables needed to make the path and to make the target follow it
    public static Node[] path;
    public int currentNodeIndex;
    public int previousNodeIndex;
    public int nextNodeIndex;

    public Vector3 currentNodePosition;
    public Vector3 previousNodePosition;
    public Vector3 nextNodePosition;
    public Vector3 targetPosition;
    public Vector3 pseudoNodePosition;

    public float movingSpeed;

    float angleIncrementing;
    float angleRemaining;
    float angle;

    bool justReachANode;

    void Start()
    {
        path = Path.path;

        //currentNodeIndex = 0;

        angleIncrementing = 0f;

    }

    public void MoveTarget(Vector3 currentNodePosition, Vector3 targetPosition)
    {
        gameObject.transform.position += ((currentNodePosition - targetPosition).normalized * Time.deltaTime * movingSpeed);
    }

    public float SetDirection()
    {
        Vector3 from = currentNodePosition - previousNodePosition;
        Vector3 to = nextNodePosition - currentNodePosition;
        float angle = Vector3.Angle(from, to);
        return angle;
    }

    
    public float RotateTarget(float increment)
    {
        if(increment >= 0)
        {
            gameObject.transform.Rotate(Vector3.forward, Time.deltaTime * 150, Space.World);
            increment += Time.deltaTime * 150;

            return increment;
        }
        else
        {
            gameObject.transform.Rotate(Vector3.forward, Time.deltaTime * -70, Space.World);
            return -1;
        }
        //Vector3 from = currentNodePosition - previousNodePosition;
        //Vector3 to = nextNodePosition - currentNodePosition;

        //Debug.Log("for " + currentNodeIndex + " previous node " + previousNodeIndex + " next node " + nextNodeIndex);
        //Debug.Log(from);
        //Debug.Log(to);
        //float angle = Vector3.Angle(from, to);
        //Debug.Log(angle);

       
        /*
        float angleIncrementing = 0;
        float angleRemaining = angle - angleIncrementing;
        while(angleRemaining > 0)
        {
            angleIncrementing = angleIncrementing + Time.deltaTime;
            //Debug.Log(angleIncrementing);
            gameObject.transform.Rotate(Vector3.forward, Time.deltaTime, Space.World);
            angleRemaining = angle - angleIncrementing;
        }
        */
        //gameObject.transform.Rotate(Vector3.forward, angle, Space.World);
    }
    

    public void RotateTargetPrimary()
    {
        gameObject.transform.Rotate(Vector3.forward, -90, Space.World);
    }

    public void CheckNodeIndex()
    {
        if (currentNodeIndex == 0)
        {
            previousNodeIndex = 7;
        }
        else
        {
            previousNodeIndex = currentNodeIndex - 1;
        }

        if (currentNodeIndex == 7)
        {
            nextNodeIndex = 0;
        }
        else
        {
            nextNodeIndex = currentNodeIndex + 1;
        }

        currentNodePosition = Path.SetCurrentNodePosition(currentNodeIndex);
        previousNodePosition = Path.SetCurrentNodePosition(previousNodeIndex);
        nextNodePosition = Path.SetCurrentNodePosition(nextNodeIndex);
    }

    public void SetCurrentNodeIndex(int n)
    {
        currentNodeIndex = n;
    }

    /* 
     * in the update, the traget's position will be compared to the current node's position
     * if the positions are not equal then the target will be movd to the current node's position
     * if the positions are equal then update/increment the current Node and set new position
     *                            move the target to the new current position
     */
    void LateUpdate()
    {
        //GameObject target = Instantiate(sampleTarget, new Vector3(xPosition, xPosition, sampleTarget.transform.position.z), sampleTarget.transform.rotation);

        CheckNodeIndex();

        targetPosition = gameObject.transform.position;
        //Debug.Log("the position of target is " + targetPosition);
        //Debug.Log("the position of current node is " + currentNodePosition);
        //Debug.Log(path[currentNodeIndex].gameObject.name);

        pseudoNodePosition = new Vector3((currentNodePosition.x - targetPosition.x), (currentNodePosition.y - targetPosition.y), 0.01f);

        float xDifference = Mathf.Abs(pseudoNodePosition.x);
        float yDifference = Mathf.Abs(pseudoNodePosition.y);

        if (targetPosition != currentNodePosition &&  xDifference> 0.05f || yDifference> 0.05f)
        {
            //Debug.Log("target position is not equal to current node's position");
            //Debug.Log(currentNodeIndex);
            //target.transform.Translate((currentNodePosition - targetPosition).normalized * Time.deltaTime * movingSpeed);
            MoveTarget(currentNodePosition, targetPosition);
        }
        else 
        {
            //Debug.Log("target position is equal to current node's position");
            //Debug.Log("index was " + currentNodeIndex);
            angle = SetDirection();
            //angleIncrementing = Time.deltaTime;
            if(angleIncrementing < angle)
            {
                angleIncrementing = RotateTarget(angleIncrementing);
            }
            if(angleIncrementing > angle)
            {
                RotateTarget(-1);
            }
            
            //Debug.Log(angleIncrementing);
            //angleIncrementing += Time.deltaTime;
            if(angleIncrementing >= angle)
            {
                angleIncrementing = 0;
                if (currentNodeIndex < path.Length - 1)
                {
                    currentNodeIndex++;
                    //Debug.Log("index changed to " + currentNodeIndex);
                    currentNodePosition = Path.SetCurrentNodePosition(currentNodeIndex);
                    //target.transform.Translate((currentNodePosition - targetPosition) * Time.deltaTime * movingSpeed);
                }
                else if (currentNodeIndex == path.Length - 1)
                {
                    currentNodeIndex = 0;
                    //Debug.Log("index changed to " + currentNodeIndex);
                    currentNodePosition = Path.SetCurrentNodePosition(currentNodeIndex);
                }
            }
            
        }
        
    }
}

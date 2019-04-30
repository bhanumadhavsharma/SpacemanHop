using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public GameObject objectToMove; //object that is to be moved
    public Transform startPoint; //point from which to start moving
    public Transform endPoint; //point to which finish moving
    public float moveSpeed; //how fast to move
    private Vector3 currentTarget; //whatever point we're going towards at that moment

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = endPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, currentTarget, moveSpeed*Time.deltaTime);
        if(objectToMove.transform.position == endPoint.position)
        {
            currentTarget = startPoint.position;
        }
        if (objectToMove.transform.position == startPoint.position)
        {
            currentTarget = endPoint.position;
        }
    }
}

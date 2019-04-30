using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject target; //what the camera will be following, and so all of its parameters and components
    public float followAhead; //distance of how far away from center of target camera will be
    private Vector3 targetPosition; //the position of the target, set manually due to not changing camera in positions y & z
    public float smoothing; //so the camera doesnt cut and make it jarring
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //sets the position of the target
        targetPosition = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);

        //based on which direction the target is facing, this sets "target position"
        if (target.transform.localScale.x > 0f)
        {
            targetPosition = new Vector3(targetPosition.x + followAhead, targetPosition.y, targetPosition.z); 
        } else
        {
            targetPosition = new Vector3(targetPosition.x - followAhead, targetPosition.y, targetPosition.z);
        }

        //camera position is set to target position
        //transform.position = targetPosition;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing*Time.deltaTime);
    }
}

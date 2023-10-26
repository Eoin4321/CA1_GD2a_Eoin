using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 100f;
    public float yOffset = 1f;
    //This will give us the position of player
    public Transform target;


    // Update is called once per frame
    void Update()
    {
        //Setting the newPos variable to where the player is. 
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset,-10f);
        //Changing my current position to target position
        //Slerp slowy moves from one vector to another
        //I put the curret transform.position in this as well as the new one so it will keep moving towards the new position.
        transform.position = Vector3.Slerp(transform.position,newPos,FollowSpeed*Time.deltaTime);
    }
}

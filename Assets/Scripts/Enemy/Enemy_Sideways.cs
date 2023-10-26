using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    //Setting up variables. Using SerializeField so I can edit the value in Unity
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    //Awake is called when the script is loaded
    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }
    //Reference Video where I learned how this works Unity 2D Platformer for Complete Beginners - #7 HEALTH SYSTEM by Pandemonium
    //LINK: https://www.youtube.com/watch?v=yxzg8jswZ8A&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=7
    //Update method is called every frame
    private void Update()
    {
        if(movingLeft)
        {
            //Checking to see if the object has reached the left edge.
            if (transform.position.x > leftEdge)
            {
                //If it has not reached the left edge it will keep moving towards it.
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            //If it has reached the left edge I set moving left to false.
            else
                movingLeft = false;
        }
        else
        {
            //Checking to see if the object has reached the right edge.
            //
            if (transform.position.x < rightEdge)
            {
                //If it has not reached the right edge it will keep moving towards it.
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = true;
        }
    }


    //When the object collides with another it runs this method.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Checks to see if it collided with the player. If it does it runs this.
        if(collision.tag == "Player")
        {
            //Calling the TakeDamage method  in the Health class. Sending in the damage variable so the player takes the correct amount of damage.
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}

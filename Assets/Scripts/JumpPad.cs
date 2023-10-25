using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float bounce = 20f;

    //Code referenced from How To Make 2D Jump Pads in Unity by bendux at LINK https://www.youtube.com/watch?v=0e3Ld6-RzIU

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If player collides with the gameobject runs the statement.
        if (collision.gameObject.CompareTag("Player"))
        {
            //Adds an upwards force to my character
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
        }
    }
    
    
}

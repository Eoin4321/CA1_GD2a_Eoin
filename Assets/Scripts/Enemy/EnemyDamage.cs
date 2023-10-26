using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    //Setting up my variable. Using a SerializeField so I can change the value in the Unity app.
    [SerializeField] protected float damage;

    //When the object collides with another it runs this method.
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        //If the object it collides with has the "Player" tag runs this if statement.
        if (collision.tag == "Player")
            //Runs the TakeDamage() method from the health class which takes in the damage. 
            collision.GetComponent<Health>().TakeDamage(damage);
    }


}

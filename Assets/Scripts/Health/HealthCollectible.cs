using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    
    //Setting up a SerializeField so I can edit this value in Unity
    [SerializeField] private float healthValue;
    //Reference video where I learened this. Unity 2D Platformer for Complete Beginners - #7 HEALTH SYSTEM by Pandemonium
    //Link to video https://www.youtube.com/watch?v=yxzg8jswZ8A&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=7
    //When the object collides with another it runs this method.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the object it collides with has the "Player" tag runs this if statement.
        if(collision.tag == "Player")
        {
            //Gets the Health and runs the AddHealth Method in the health script. Gives in the health pick up value
            collision.GetComponent<Health>().AddHealth(healthValue);
            //Setting the heart collectible to disappear after being taken
            gameObject.SetActive(false);
        }    
    }
}

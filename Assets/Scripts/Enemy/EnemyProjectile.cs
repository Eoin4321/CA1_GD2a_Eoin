using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This classe is a heritance class of EnemyDamage. This gives his class all its variables.
public class EnemyProjectile : EnemyDamage //Will damage the player everytime it touches. 
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    public void ActivateBullet()
    {
        //Set current lifetime of the Bullet to 0
        lifetime = 0;
        //Setting the object to active in the hierarchy.
        gameObject.SetActive(true);
    }

    private void Update()
    //Video reference where I learned this Unity 2D Platformer for Complete Beginners - #9 TRAPS. By Pandemonium
    //Link https://www.youtube.com/watch?v=5EesvCG9_FA&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=9
    {
        //setting up movement speed
        float movementSpeed = speed * Time.deltaTime;
        //Moving the object to the left by - movement speed on the x axis.
        transform.Translate(-movementSpeed, 0, 0);

        //Calculating the current Lifetime of the bullet
        lifetime += Time.deltaTime;
        //If the lifetime is greated then the reset time it will run this.
        if (lifetime > resetTime)
            //Deactivating the bullet object.
            gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Base is a keyword in C# which lets you automatically access the parent script
        base.OnTriggerEnter2D(collision); //Damages the player
        gameObject.SetActive(false) ; //When bullet hits Player/wall this will deactive
    }
}

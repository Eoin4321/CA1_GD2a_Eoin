using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //Setting up variables. Using [SerializeField] so I can edit the values in the unity app
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] bullets;
    private float cooldownTimer;

    private void Attack()
    {
        //Video reference where I learned this Unity 2D Platformer for Complete Beginners - #9 TRAPS. By Pandemonium
        //Link https://www.youtube.com/watch?v=5EesvCG9_FA&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=9
        //Setting  ColldownTimer to 0
        cooldownTimer = 0;

        //Everytime I shoot I reset the projectile to the firePoint Position which is infront of the turret.
        bullets[FindBullet()].transform.position = firePoint.position;
        //Finding a bullet and in the array list and running the Activate Bullet method in the EnemyProjectile Script.
        bullets[FindBullet()].GetComponent<EnemyProjectile>().ActivateBullet();
    }

    private int FindBullet()
    {
        //Running through my array List of bullets
        for (int i = 0; i < bullets.Length; i++)
        {
            //Returning the first one that is not active in the Hierarchy
            if (!bullets[i].activeInHierarchy)
                return i;
        }
        //If it cant find any non active bullets it will just send back the first one.
        return 0;
    }

    private void Update()
    {
        //Having the cooldownTimer count up.
        cooldownTimer += Time.deltaTime;
        //If the cooldownTimer is greater then or equal to attack Cooldown it will let the turret attack 
        if (cooldownTimer >= attackCooldown)
            Attack();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//UNUSED PLAYER ATTACK SCRIPT 
//Reference video where I learned this Unity 2D Platformer for Complete Beginners - #4 SHOOTING by Pandemonium
//LINK TO VIDEO https://www.youtube.com/watch?v=PUpC44Q64zY&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=4
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] bullets;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;



    private void Awake()
    {
        //
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && cooldownTimer > attackCooldown)
        Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        if (5 == (int)playerMovement.currentsize)
        {
            anim.SetTrigger("attack");
            cooldownTimer = 0;

            bullets[FindBullet()].transform.position = firePoint.position;
            bullets[FindBullet()].GetComponent<Bullet>().SetDirection(Mathf.Sign(transform.localScale.x));
        }
  
    }

    private int FindBullet()
    {
        for(int i =0; i < bullets.Length; i++)
        {
            if(!bullets[i].activeInHierarchy)
            return i;
        }
        return 0;
    }
}

    

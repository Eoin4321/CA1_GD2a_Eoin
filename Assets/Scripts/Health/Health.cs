using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    //Setting up a header so my unity is better organised.
    [Header ("Health")]
    //Using a SerializeField so I can edit startingHealth in the Unity app without opening VS code
    [SerializeField] private float startingHealth;
   
    //Get private set lets the code be accessed from other classes but can only be edited in this class
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    //Setting up a header so my unity is better organised.
    [Header("Invinibility Frames")]
    //Setting up variables for invincibility.
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfflashes;
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        //Setting current health to be the same as starting health.
        currentHealth = startingHealth;
        //Geting references for my animator and spriterenderer.
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage)
    {
        //Mathf.Clamp sets a minimum and maximum value for my current health. The current health when taking damage can stay between 0 and the starting health which is 3
        //Reference video for where I learned this Unity 2D Platformer for Complete Beginners - #7 HEALTH SYSTEM by Pandemonium. LINK: https://www.youtube.com/watch?v=yxzg8jswZ8A&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=7
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        //If my player takes damage but is not dead as their health is greater then 0
        if(currentHealth >0)
        {
            //player damaged;
            anim.SetTrigger("hurt");
            //calling my IEnumerator Invunerability(). You have to use StartCorutine to call this method
            StartCoroutine(Invunerability());
            //Infinibility frames
        }
        else
        {
            //If player is dead
            if (!dead)
            {
                //Getting a reference to PlayerMovement
                if (GetComponent<PlayerMovement>() != null)
                {
                    //Play the trigger for the hurt animation
                    anim.SetTrigger("hurt");
                    //Remove being able to move from the player
                    GetComponent<PlayerMovement>().enabled = false;
                    //Set dead to true
                    dead = true;
                    //Change scene to the game over scene.
                    SceneManager.LoadScene("GameOver");
                }

            }
        }
    }
    //Method to add health. Taking in the value of health healed.
    public void AddHealth(float _value)
    {
        //Using Mathf so my players health doesnt go above starting health.
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    //Invunerability system Reference Video Unity 2D Platformer for Complete Beginners - #8 IFRAMES By Pandemonium. LINK https://www.youtube.com/watch?v=YSzmCf_L2cE&t=226
    private IEnumerator Invunerability()
    {
        //Setting it to my Player will Ignore Collisions with objectss on layer 11.
        Physics2D.IgnoreLayerCollision(10,11, true);
        //Making my Player flash red when they take damage.
        //Runing a for loop for each flash
        for (int i = 0; i < numberOfflashes; i++) 
        {
            //changing Player colour to red
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            //As imm using an IEnumerator I can make the code wait before running the next line. I use the IframesDuration and divide it by the number of flashes I multiple it by 2 as Im flashing twice as I need to change the colour twice.
            yield return new WaitForSeconds(iFramesDuration / (numberOfflashes * 2));
            //Changing my colour to white so my character flashes red 
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfflashes * 2));
        }
        //Turning back on Player Collisions with objects in layer 11.
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }
}

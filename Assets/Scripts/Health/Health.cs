using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
   
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("Invinibility Frames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfflashes;
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth >0)
        {
            //player damaged;
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            //Infinibility frames
        }
        else
        {
            if (!dead)
            {
                //player dies
                if (GetComponent<PlayerMovement>() != null)
                {
                    anim.SetTrigger("hurt");
                    GetComponent<PlayerMovement>().enabled = false;
                    dead = true;
                    SceneManager.LoadScene("GameOver");
                }

            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(10,11, true);
        for (int i = 0; i < numberOfflashes; i++) 
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfflashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfflashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }
}

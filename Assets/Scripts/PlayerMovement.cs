using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool Grounded;

    //Awake is called when the scipt is being loaded and then the method is ran;
    private void Awake()
    {
        //GetComponent will get the Rigidbody2D/Animator and store it for references.
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //This stores the HorizontalInput
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput*speed,body.velocity.y);
      
        //Checking to see what way the player is facing if they are facing left it will
        //change the sprite to look the other way.
        if (horizontalInput >0.01f)
            transform.localScale = new Vector3(3,3,1);
        else if(horizontalInput < -0.01f)
            transform.localScale = new Vector3(-3,3,1);

        if (Input.GetKey(KeyCode.Space) && Grounded)
            Jump();

        //Set animator parameters 
        //If player is standing still horizontalInput is 0 so it will set run to true and play the run animation
        //If it is false it will play the idle animation
        anim.SetBool("Run",horizontalInput !=0);
        anim.SetBool("Grounded", Grounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
        Grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            Grounded = true;
    }
}

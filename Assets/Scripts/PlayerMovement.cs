using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //SerializeField lets me edit the variable in the unity app.
    //Settingn up variables
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform roofCheck;
    [SerializeField] private LayerMask roofLayer;
    [SerializeField] private TrailRenderer tr;

    //Setting up the variable audio manager linked with the audio manager class.
    AudioManager audioManager;


    //UNUSED DASH 
    /*private bool canDash = true;
    [SerializeField] private bool isDashing;
    [SerializeField] private float dashingPower;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
    */


    //A header creates a visual header in the unity app to make it more readable.
    [Header("Coyote Time")]
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    //Storing a refernence to components and setting them to a variable.
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    //Setting up variables
    private float horizontalInput;
    public float currentsize;




    //Awake is called when the scipt is being loaded and then the method is ran;
    private void Awake()
    {
        //Setting the AudioManger to a variable.
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        //GetComponent will get the Rigidbody2D/Animator/BoxCollider and store it for references.
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        //Storing the players current size
        currentsize = 5;
       
    }
    

    // Update is called once per frame
    void Update()
    {
        //UNUSED DASHING CODE
        //If player is dashing locks out of other movements
        /*
        if(isDashing)
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.Mouse1) && canDash)
        {
            StartCoroutine(Dash());
        }
        */
        //This stores the HorizontalInput

        horizontalInput = Input.GetAxis("Horizontal");

        //If player is grounded resets coyoteTime;
        if (isGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        //If they are not grounded counts down. This gives the player a breif chance to jump after falling off an edge
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        //Sets the movement based on HorizontalInput. The HorizontalInput is * by the speed to control the player moving left or right. 
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        //Checking to see what way the player is facing if they are facing left it will
        //change the sprite to look the other way.
        if (horizontalInput > 0.01f)
        {
           
            transform.localScale = new Vector3(currentsize, currentsize, 1);
        }
        else if (horizontalInput < -0.01f)
        {
            
            transform.localScale = new Vector3(-(currentsize), currentsize, 1);
        }

        //If the player clicks space and CoyoteTimeCOunter > 0 which means they are within the grace period of coyate time it will run the jump method.
        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0)
            Jump();

        //Adds a weighted jump as the player stops going up if they let go of space
        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
        { 
        body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);
        coyoteTimeCounter = 0f;//Prevents player from double jumping by spamming the jump button
        }
        //Controlling my characters size. If size is ==5 (Big) they will shrink else they will grow
        if (Input.GetKeyDown(KeyCode.LeftShift) && currentsize == 5)
        {
            audioManager.PlaySFX(audioManager.shrink);
            //Adjusting speed and size based on what mode the player is in 
            speed = 9;
            currentsize = 2;
            transform.localScale = new Vector3(currentsize, currentsize, 1);
           
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && currentsize == 2 && isWall() == false && isRoof() == false)
        {
            audioManager.PlaySFX(audioManager.shrink);
            speed = 14;
            currentsize = 5;
            transform.localScale = new Vector3(currentsize, currentsize, 1);
            
        }

            //Set animator parameters 
            //If player is standing still horizontalInput is 0 so it will set run to true and play the run animation
            //If it is false it will play the idle animation
            anim.SetBool("Run",horizontalInput !=0);
        anim.SetBool("Grounded", isGrounded());
    }

    //Jump method
    private void Jump()
    {
        //Updating the vertical velocity of my player. This lets my player jump.
        body.velocity = new Vector2(body.velocity.x, jumpPower);
        //Playing a sound effect when I jump.
        audioManager.PlaySFX(audioManager.jump);
        
       
    }
    //Checking if the player is grounded.
    private bool isGrounded()
    {
        //Checking if player is in contact with ground by seeing if there is a collision with the circular area of the ground check and the ground layer.//0.2f controls the radius 
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    //Checking if player is near a wall
    private bool isWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    //Checking if player is near a roof.
    private bool isRoof()
    {
        return Physics2D.OverlapCircle(roofCheck.position, 0.2f, groundLayer);
    }
    //UNUSED PLAYER ATTACK
   /* 
    public bool canAttack()
    {
        return isGrounded();
    }*/

    //UNUSED PLAYER DASH 
    /*private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = body.gravityScale;
        body.gravityScale = 0f;
        body.velocity = new 
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        body.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        
        canDash = true;


    }*/
}

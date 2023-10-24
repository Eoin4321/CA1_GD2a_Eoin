using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform roofCheck;
    [SerializeField] private LayerMask roofLayer;
    [SerializeField] private TrailRenderer tr;

    /*private bool canDash = true;
    [SerializeField] private bool isDashing;
    [SerializeField] private float dashingPower;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
    */


    [Header("Coyote Time")]
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    public float currentsize;




    //Awake is called when the scipt is being loaded and then the method is ran;
    private void Awake()
    {
        //GetComponent will get the Rigidbody2D/Animator and store it for references.
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        currentsize = 5;
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

        if (isGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        //Checking to see what way the player is facing if they are facing left it will
        //change the sprite to look the other way.
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(currentsize, currentsize, 1);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-(currentsize), currentsize, 1);

        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0)
            Jump();

        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
        { 
        body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);
        coyoteTimeCounter = 0f;//Prevents player from double jumping by spamming the jump button
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && currentsize == 5)
        {
            currentsize = 2;
            transform.localScale = new Vector3(currentsize, currentsize, 1);
           
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && currentsize == 2 && isWall() == false && isRoof() == false)
        {
            currentsize = 5;
            transform.localScale = new Vector3(currentsize, currentsize, 1);
            
        }

            //Set animator parameters 
            //If player is standing still horizontalInput is 0 so it will set run to true and play the run animation
            //If it is false it will play the idle animation
            anim.SetBool("Run",horizontalInput !=0);
        anim.SetBool("Grounded", isGrounded());
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpPower);
        
       
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool isWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private bool isRoof()
    {
        return Physics2D.OverlapCircle(roofCheck.position, 0.2f, groundLayer);
    }

    public bool canAttack()
    {
        return isGrounded();
    }

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

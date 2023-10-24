using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private float jumpPower;
    [SerializeField]private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
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
        //This stores the HorizontalInput
        horizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        //Checking to see what way the player is facing if they are facing left it will
        //change the sprite to look the other way.
        if (horizontalInput >0.01f)
            transform.localScale = new Vector3(currentsize,currentsize,1);
        else if(horizontalInput < -0.01f)
            transform.localScale = new Vector3(-(currentsize),currentsize,1);

        if (Input.GetKey(KeyCode.Space) && isGrounded())
            Jump();

        if (Input.GetKeyDown(KeyCode.LeftShift) && currentsize == 5 && isWall() == false)
        {
            currentsize = 2;
            transform.localScale = new Vector3(currentsize, currentsize, 1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && currentsize == 2 && isWall()==false)
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
        anim.SetTrigger("jump");
       
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool isWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return isGrounded();
    }
}

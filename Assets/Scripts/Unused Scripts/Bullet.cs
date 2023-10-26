using System;
using UnityEngine;

//UNUSED BULLET SCRIPT.
//Reference video Where I learned this Unity 2D Platformer for Complete Beginners - #4 SHOOTING By Pandemonium
//LINK TO VIDEO https://www.youtube.com/watch?v=PUpC44Q64zY&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=4
public class Bullet : MonoBehaviour
{
     [SerializeField] private float speed;
     private float direction;
     private bool hit;
     private float lifetime;

     private BoxCollider2D boxCollider;
     private Animator anim;

     private void Awake()
     {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
     }
     private void Update()
     {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime*direction;
        transform.Translate(movementSpeed,0,0);

        lifetime += Time.deltaTime;
        if(lifetime > 5) gameObject.SetActive(false);
     }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled =false;
        anim.SetTrigger("explode");

        if (collision.tag == "Enemy")
            collision.GetComponent < Health>().TakeDamage(1) ;
    }

    public void SetDirection(float _direction)
    {
        lifetime =0;
        direction=_direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled =true;

        float localScaleX= transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
        localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

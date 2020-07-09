using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected Rigidbody2D charRb;
    protected float health = 100;
    
    // variables for checking how environment interacts with player
    protected int charSize;
    protected int charType;
    
    //variables for movement
    protected float maxSpeed;
    [SerializeField]
    protected float acceleration;
    // protected float deceleration;

    //how close to mouse posiiton to stop moving
    protected float xRange = 0.2f;

    protected float jumpMultiplier;
    // protected float jumpFallMultiplier;
    // protected float mass;
    // protected float linearDrag;
    // protected float gravityScale;


    //variable for attack
    // protected hitbox charHitbox 
    // protected hitbox charHurtbox
    protected float damage;
    protected float defense;

    //variables for extra mobility
    protected bool canSwim = false;
    protected bool canClimb = false;

    // variables for display
    protected Sprite charSprite;
    protected Animator charAnimator;




    // When player manager switches to using this controller
    public virtual void OnSwitch(GameObject player)
    {
        //change everything about the scene componenets
        //rigidbody
        charRb = player.GetComponent<Rigidbody2D>();
        charRb.bodyType = RigidbodyType2D.Dynamic;
        //sprite
        //animator
        //hitbox
    }


    public virtual void Move()
    {
        Debug.Log("moving");
        if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x > charRb.position.x + xRange)
        {
            if(charRb.velocity.x < maxSpeed)
                charRb.AddForce(new Vector2(acceleration, 0f));
        }
        else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x< charRb.position.x - xRange)
        {
            if(charRb.velocity.x > -maxSpeed)
                charRb.AddForce(new Vector2(-acceleration, 0f));
        }
    }

    public virtual void Jump()
    {
        Debug.Log("jumping");
        charRb.AddForce(new Vector2(0f, jumpMultiplier));
    }

    public virtual void Attack()
    {
        Debug.Log("attacking");
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

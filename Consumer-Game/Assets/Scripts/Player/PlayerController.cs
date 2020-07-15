using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    protected Rigidbody2D charRb;
    protected float health = 100;
    
    // variables for checking how environment interacts with player
    protected int charSize;
    protected int charType;
    
    //variables for movement
   [SerializeField] protected float maxSpeed = 10f;
    

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

    //Player Physics Variables
    protected float playerVelocityX = 0f;
    protected float playerVelocityY = 0f;
    protected bool movingRight = false;
    protected bool movingLeft = false;
    protected Vector2 normalVector = new Vector2(0f, 1f);
    protected Vector2 perpendicularVector = new Vector2(1f, 0f);
    [SerializeField] protected float deacceleration = 10f;
    [SerializeField] protected float acceleration = 5f;



    // When player manager switches to using this controller
    public virtual void OnSwitch(GameObject player)
    {
        //change everything about the scene componenets
        //rigidbody
        charRb = player.GetComponent<Rigidbody2D>();
        //charRb.bodyType = RigidbodyType2D.Dynamic;
        //sprite
        //animator
        //hitbox
    }


    public virtual void Move()
    {
        

        if (Input.GetButton("Move")){
            //Right Direction
            if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x > charRb.position.x + xRange)
            {
                movingRight = true;
                movingLeft = false;
                    
            }
            //Left Direction
            else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x< charRb.position.x - xRange)
            {
                movingRight = false;
                movingLeft = true;
            }
            else {
                movingLeft = false;
                movingRight = false;
            }
        }
        else{
            movingLeft = false;
            movingRight = false;
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
    
    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public virtual void FixedUpdate() {

        if (movingRight && Vector2.Dot(charRb.velocity,perpendicularVector) < 0){
            playerVelocityX = 0;
        }
        else if (movingRight && Vector2.Dot(charRb.velocity,perpendicularVector) >= 0){
            playerVelocityX = Mathf.Clamp(playerVelocityX + acceleration, 0f, maxSpeed);
        }
        else if (movingLeft && Vector2.Dot(charRb.velocity,perpendicularVector) > 0){
            playerVelocityX = 0;
        }
        else if (movingLeft && Vector2.Dot(charRb.velocity,perpendicularVector) <= 0){
            playerVelocityX = Mathf.Clamp(playerVelocityX - acceleration, -maxSpeed, 0f);
        }
        else if (!movingLeft && !movingRight){
            if (playerVelocityX < 0){
                playerVelocityX = Mathf.Clamp(playerVelocityX + deacceleration,-maxSpeed, 0f);
            }
            else if (playerVelocityX > 0){
                playerVelocityX = Mathf.Clamp(playerVelocityX - deacceleration, 0f, maxSpeed);
            }
        }


        //Debug.Log(charRb);
        charRb.velocity = new Vector2 (playerVelocityX, playerVelocityY);
    }
}

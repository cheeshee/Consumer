using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    protected Rigidbody2D charRb;
    protected BoxCollider2D charBoxCol;
    protected ContactFilter2D contactFilter;
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
    protected bool isGrounded = false;
    protected Vector2 colliderSize;
    protected Vector2 groundCheckPosition;
    protected PlayerManager playerManagerComp;
    protected float groundCheckRadius = 0.1f;
    protected LayerMask playerLayerMask;
    protected Vector2 playerScale;


    // When player manager switches to using this controller
    public virtual void OnSwitch(GameObject player)
    {

        contactFilter.useTriggers = false; //Won't check collisions against triggers
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(LayerMask.NameToLayer("Player"))); 
        //use physics2D settings to determine what layers we're going to check collisions against
        contactFilter.useLayerMask = true;
        //change everything about the scene componenets
        //rigidbody
        playerLayerMask = Physics2D.GetLayerCollisionMask(LayerMask.NameToLayer("Player"));
        charRb = player.GetComponent<Rigidbody2D>();
        charBoxCol = player.GetComponent<BoxCollider2D>();
        playerManagerComp = player.GetComponent<PlayerManager>();
        colliderSize = charBoxCol.size;
        playerScale = playerManagerComp.GetLocalScale();
        //charRb.bodyType = RigidbodyType2D.Dynamic;
        //sprite
        //animator
        //hitbox
    }


    public virtual void Move()
    {
        

        if (Input.GetButton("Move")){
            Debug.Log("moving");
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

        ComputeHorizontalVelocity();
        GroundCheck();
        Debug.Log(isGrounded);

        //Debug.Log(charRb);
        charRb.velocity = new Vector2 (playerVelocityX, playerVelocityY);
    }


    protected virtual void ComputeHorizontalVelocity() {
        
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
    }


    protected virtual void SlopeCheck(){
        
    }

    protected virtual void GroundCheck(){
        Vector2 currentPosition = playerManagerComp.GetPosition();

        groundCheckPosition = new Vector2(currentPosition.x, currentPosition.y - (colliderSize.y / 2 * playerScale.y));
        Debug.Log(groundCheckPosition);
        //Debug.Log(colliderSize);
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition, groundCheckRadius, playerLayerMask);
    }

}

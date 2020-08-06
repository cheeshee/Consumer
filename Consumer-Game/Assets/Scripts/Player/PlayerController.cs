using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: HealthInterface
{
    // referring to the instances on runtime
    protected Rigidbody2D charRb;
    protected CapsuleCollider2D charCollider;
    protected ContactFilter2D contactFilter;

    
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

    // variables for display and scene
    protected Sprite storedSprite;
    protected Animator storedAnimator;
    protected Rigidbody2D storedRb;
    protected CapsuleCollider2D storedCollider;

    //Player Physics Variables
    protected float playerVelocityX = 0f;
    protected float playerVelocityY = 0f;
    protected bool movingRight = false;
    protected bool movingLeft = false;
    protected Vector2 normalVector = new Vector2(0f, 1f);
    protected Vector2 slopePerpendicularVector = new Vector2(1f, 0f);
    protected float slopeDownAngle;
    protected float slopeDownAngleOld;
    protected float slopeSideAngle;
    protected float maxSlopeAngle = 45;
    [SerializeField] protected float deacceleration = 10f;
    [SerializeField] protected float acceleration = 5f;
    protected bool isGrounded = false;
    protected bool isOnSlope = false;
    protected bool isJumping = false;
    protected bool canJump = false;
    protected bool jumpNextFixedUpdate = false;
    protected bool canWalkOnSlope = false;
    protected Vector2 colliderSize;
    protected Vector2 groundCheckPosition;
    protected PlayerManager playerManagerComp;
    protected float groundCheckRadius = 0.5f;
    protected LayerMask playerLayerMask;
    protected Vector2 playerScale;
    protected Vector2 currentPosition;
    protected float slopeCheckDistance = 0.5f;
    protected float jumpVelocity = 4f;
    protected float jumpingFloatModifier = 0.5f;
    protected float initialGravityModifier = 1f;


    //Health
    [HideInInspector] public float health { get; set; }
    [HideInInspector] public float percentageHealth { get; set; }
    [SerializeField] public float maxHealth  { get; set; }
    public float storedMaxHealth;

    public virtual void InitializeHealth()
    {
        
        maxHealth = storedMaxHealth;
        health = maxHealth * percentageHealth;
    }

    public virtual void ApplyDamage(int points)
    {
        health = Mathf.Clamp(health - points, 0, maxHealth);
        percentageHealth = health / maxHealth; 
    }

    // When player manager switches to using this controller
    public virtual void OnSwitch(GameObject player)
    {

        contactFilter.useTriggers = false; //Won't check collisions against triggers
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(LayerMask.NameToLayer("Player"))); 
        //use physics2D settings to determine what layers we're going to check collisions against
        contactFilter.useLayerMask = true;
        //change everything about the scene componenets
        
        charRb = player.GetComponent<Rigidbody2D>();
        charCollider = player.GetComponent<CapsuleCollider2D>();
        playerManagerComp = player.GetComponent<PlayerManager>();
        colliderSize = charCollider.size;
        playerScale = playerManagerComp.GetLocalScale();
        playerLayerMask = playerManagerComp.GetPlayerLayerMask();
        //charRb.bodyType = RigidbodyType2D.Dynamic;

        //sprite
        player.GetComponent<SpriteRenderer>().sprite = storedSprite;
        //animator
        player.GetComponent<Animator>().runtimeAnimatorController = storedAnimator.runtimeAnimatorController;
        //rigidbody
        CopyRigidBody(charRb, storedRb);
        //collider
        CopyCollider(charCollider, storedCollider);

        InitializeHealth();
    }

    private void CopyRigidBody(Rigidbody2D copyTo, Rigidbody2D copyFrom){
        copyTo.bodyType = copyFrom.bodyType;
        copyTo.sharedMaterial = copyFrom.sharedMaterial;
        copyTo.simulated = copyFrom.simulated;
        copyTo.useAutoMass = copyFrom.useAutoMass;
        copyTo.mass = copyFrom.mass;
        copyTo.angularDrag = copyFrom.angularDrag;
        copyTo.drag = copyFrom.drag;
        copyTo.gravityScale = copyFrom.gravityScale;
        copyTo.collisionDetectionMode = copyFrom.collisionDetectionMode;

    }

    private void CopyCollider(CapsuleCollider2D copyTo, CapsuleCollider2D copyFrom){
        copyTo.sharedMaterial = copyFrom.sharedMaterial;
        copyTo.direction = copyFrom.direction;
        copyTo.offset = copyFrom.offset;
        copyTo.size = copyFrom.size;
    }


    public virtual void Move()
    {
        

        if (Input.GetButton("Move")){
            //Right Direction
            if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x > charRb.position.x + xRange)
            {
                movingRight = true;
                movingLeft = false;
                playerManagerComp.FlipHorizontal(true);
                    
            }
            //Left Direction
            else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x< charRb.position.x - xRange)
            {
                movingRight = false;
                movingLeft = true;
                playerManagerComp.FlipHorizontal(false);
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
        if (Input.GetButton("Jump")){
            if(canJump){
                canJump = false;
                isJumping = true;
                jumpNextFixedUpdate = true;
                charRb.gravityScale = jumpingFloatModifier;
            }
            else if (isJumping){
                charRb.gravityScale = jumpingFloatModifier;
            }
            else if (!isJumping){
                charRb.gravityScale = initialGravityModifier;
            }
        }
        else {
            charRb.gravityScale = initialGravityModifier;
        }
        
    }

    public virtual void Attack()
    {
        //Debug.Log("attacking");
    }
    
    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public virtual void FixedUpdate() {
        currentPosition = playerManagerComp.GetPosition();
        groundCheckPosition = new Vector2(currentPosition.x, currentPosition.y - (colliderSize.y / 2 * playerScale.y));
        
        GroundCheck();
        SlopeCheck();
        ComputeVelocity();
        //Debug.Log(isGrounded);

        //Debug.Log(charRb);
        
    }


    protected virtual void ComputeHorizontalVelocity() {
        
        if (movingRight && Vector2.Dot(charRb.velocity,slopePerpendicularVector) < 0){
            playerVelocityX = acceleration;
        }
        else if (movingRight && Vector2.Dot(charRb.velocity,slopePerpendicularVector) >= 0){
            playerVelocityX = Mathf.Clamp(playerVelocityX + acceleration, 0f, maxSpeed);
        }
        else if (movingLeft && Vector2.Dot(charRb.velocity,slopePerpendicularVector) > 0){
            playerVelocityX = -acceleration;
        }
        else if (movingLeft && Vector2.Dot(charRb.velocity,slopePerpendicularVector) <= 0){
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



    protected virtual void ComputeVelocity(){
        ComputeHorizontalVelocity();
        Debug.Log("Grounded:" + isGrounded + " " + "OnSlope:" + isOnSlope + " " + "isJumping:" + isJumping + " " + "canJump:" + canJump + " " + "jumpNextUpdate" + jumpNextFixedUpdate);

        if (isGrounded && jumpNextFixedUpdate){
            charRb.AddForce(new Vector2 (0f, jumpVelocity), ForceMode2D.Impulse);
            jumpNextFixedUpdate = false;
        }
        else if (isGrounded && !isOnSlope && !isJumping) //if not on slope
        {
            charRb.velocity = new Vector2 (playerVelocityX, charRb.velocity.y);
        }
        else if (isGrounded && isOnSlope && !isJumping) //If on slope
        {
            charRb.velocity = new Vector2 (playerVelocityX * slopePerpendicularVector.x, playerVelocityX * slopePerpendicularVector.y);
        }
        else if (!isGrounded) //If in air
        {
            charRb.velocity = new Vector2 (playerVelocityX, charRb.velocity.y);
        }
        //TODO

        
    }


    protected virtual void SlopeCheck(){
        SlopeCheckHorizontal();
        SlopeCheckVertical();
    }


    protected virtual void SlopeCheckHorizontal(){
        RaycastHit2D slopeHitFront = Physics2D.Raycast(groundCheckPosition, Vector2.right, slopeCheckDistance, playerLayerMask);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(groundCheckPosition, Vector2.left, slopeCheckDistance, playerLayerMask);

        if (slopeHitFront)
        {
            isOnSlope = true;

            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);

        }
        else if (slopeHitBack)
        {
            isOnSlope = true;

            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }
    }


    protected virtual void SlopeCheckVertical(){
        RaycastHit2D hit = Physics2D.Raycast(groundCheckPosition, Vector2.down, slopeCheckDistance, playerLayerMask);
        if (hit){
                slopePerpendicularVector = PerpVector(hit.normal);
                slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

                if(slopeDownAngle != slopeDownAngleOld){
                    isOnSlope = true;
                }                       

            slopeDownAngleOld = slopeDownAngle; 
            Debug.DrawRay(hit.point, slopePerpendicularVector, Color.blue);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
           // Debug.Log(LayerMask.LayerToName(hit.collider.gameObject.layer));
        }

        if (slopeDownAngle > maxSlopeAngle || slopeSideAngle > maxSlopeAngle)
        {
            canWalkOnSlope = false;
        }
        else
        {
            canWalkOnSlope = true;
        }

        charRb.sharedMaterial = playerManagerComp.GetFriction(isOnSlope && canWalkOnSlope && !movingRight && !movingLeft);
        
    }

    protected virtual void GroundCheck(){
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition, groundCheckRadius, playerLayerMask);
        
        if(charRb.velocity.y <= 0.0f)
        {
            isJumping = false;
        }

        if(isGrounded && !isJumping && slopeDownAngle <= maxSlopeAngle)
        {
            canJump = true;
        }
   }

    protected virtual Vector2 PerpVector(Vector2 vector){
        return new Vector2(vector.y, -vector.x);
    }

    public virtual int GetCharType(){
        return charType;
    }

}

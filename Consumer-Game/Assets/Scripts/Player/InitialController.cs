using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialController : PlayerController
{
    private Rigidbody2D charRb2;
    private float health = 100;
    
    // variables for checking how environment interacts with player
    private int charSize;
    private int charType;
    
    //variables for movement
    private float maxSpeed;
    private float acceleration;
    private float deceleration;

    private float jumpFloatMultiplier;
    private float jumpFallMultiplier;
    // private float mass;
    // private float linearDrag;
    // private float gravityScale;


    //variable for attack
    // private hitbox charHitbox 
    // private hitbox charHurtbox
    private float damage;
    private float defense;

    //variables for extra mobility
    private bool canSwim = false;
    private bool canClimb = false;

    // variables for display
    private Sprite charSprite;
    private Animator charAnimator;

    // constructor
    public InitialController(GameObject player) 
    {
        OnSwitch(player);
    }

    // When player manager switches to using this controller
    public override void OnSwitch(GameObject player)
    {
        //change everything about the scene componenets
        //rigidbody
        charRb2 = player.GetComponent<Rigidbody2D>();
        charRb2.bodyType = RigidbodyType2D.Dynamic;
        //sprite
        //animator
        //hitbox

    }

    public override void Move()
    {
        base.Move();
    }

    public override void Jump()
    {
        base.Jump();
        charRb2.AddForce(new Vector2(0f, 100f));
    }

    public override void Attack()
    {
        base.Attack();

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

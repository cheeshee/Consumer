using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestHerbivoreController : PlayerController
{
    // constructor
    public ForestHerbivoreController(GameObject sourceCharacter){
        //set all the variables for this controller
        health = 100;

        charSize = (int) PlayerProperties.Size.NORMAL;
        charType = (int) PlayerProperties.CharacterType.ANIMAL;
        
        maxSpeed = 5f;
        acceleration = 15f;
        // deceleration

        jumpMultiplier = 200f;
        // jumpFallMultiplier
        // mass 
        // linearDrag
        // gravityScale

        // charHitbox
        // charHurtbox
        // damage
        // defense

        // canSwim
        // canClimb

        // charSprite  
        storedSprite = sourceCharacter.GetComponent<SpriteRenderer>().sprite;
        // charAnimator
        storedAnimator = sourceCharacter.GetComponent<Animator>();
        //rigidbody
        storedRb = sourceCharacter.GetComponent<Rigidbody2D>();
        //collider
        storedCollider = sourceCharacter.GetComponent<CapsuleCollider2D>();

    }

    // When player manager switches to using this controller
    public override void OnSwitch(GameObject player)
    {
        base.OnSwitch(player);
    }

    // public override void Move()
    // {
    //     base.Move();
    // }

    // public override void Jump()
    // {
    //     base.Jump();
    // }

    // public override void Attack()
    // {
    //     base.Attack();

    // }
    

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }


    public override void FixedUpdate(){
        base.FixedUpdate();
    }
}

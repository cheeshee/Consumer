﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerController : PlayerController
{
    // constructor
    public VillagerController(GameObject sourceCharacter) 
    {
        //set all the variables for this controller
        health = 100;

        charSize = (int) PlayerProperties.Size.NORMAL;
        charType = (int) PlayerProperties.CharacterType.VILLAGER;
        
        maxSpeed = 5f;
        acceleration = 10f;
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

        NpcAi temp = sourceCharacter.GetComponent<NpcAi>();

        storedMaxHealth = temp.maxHealth;

    }

    // When player manager switches to using this controller
    public override void OnSwitch(GameObject player)
    {
        base.OnSwitch(player);
        player.GetComponent<SpriteRenderer>().color = Color.green;
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

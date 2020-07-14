using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialController : PlayerController
{
    // constructor
    public InitialController(GameObject player) 
    {
        //set all the variables for this controller
        health = 100;

        charSize = (int) PlayerProperties.Size.SMALL;
        charType = (int) PlayerProperties.CharacterType.CHILD;
        
        maxSpeed = 3f;
        acceleration = 5f;
        // deceleration

        jumpMultiplier = 100f;
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
        // charAnimator
    }

    // When player manager switches to using this controller
    public override void OnSwitch(GameObject player)
    {
        base.OnSwitch(player);
         player.GetComponent<SpriteRenderer>().color = Color.white;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerController : PlayerController
{
    // constructor
    public VillagerController(GameObject player) 
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
        // charAnimator
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
    
 // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

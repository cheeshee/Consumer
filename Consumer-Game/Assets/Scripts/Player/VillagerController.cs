using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerController : PlayerController
{
    // constructor
    public VillagerController(GameObject sourceCharacter) 
    {
        //set all the variables for this controller
        //health = 100;

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

        SaveController(sourceCharacter);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialController : PlayerController
{
    // constructor
    public InitialController(GameObject sourceCharacter) 
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
        canClimb = true;

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

    public override void Attack()
    {
        base.Attack();
        AttackPooler.Instance.SpawnFromPool("InitialBasicAttack", new Vector3(0.41f, -0.191f, 0f), Quaternion.identity, playerManagerComp.GetGameObject(), true);
        Debug.Log("attacked");    
    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }


    public override void FixedUpdate(){
        base.FixedUpdate();
    }
}

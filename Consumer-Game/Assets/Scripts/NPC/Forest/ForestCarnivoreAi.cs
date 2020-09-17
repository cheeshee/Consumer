using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestCarnivoreAi : PacingNpcAi
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // THESE ARE CUSTOM
        charType = (int)PlayerProperties.CharacterType.ANIMAL;
        speed = 50f;
        npcController = new ForestCarnivoreController(gameObject);
        // TODO: Health and other variables
        health = 70;
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    // NPC AI overrides
    public override void CheckOutPlayer(){
        if (playerScript.GetCharType() < (int) PlayerProperties.CharacterType.ANIMAL){
            // in case of villager or child, attack
            onPath = false;
        } // else do nothing, continue pacing
    }

    protected override void ReactToPlayer(){
        direction = (playerScript.GetPosition() - npcRb.position).normalized;
        velocityX = 1.4f * direction.x * speed * Time.deltaTime;
        npcRb.velocity = new Vector2(velocityX, npcRb.velocity.y);
        // TODO: attack player??
    }
}

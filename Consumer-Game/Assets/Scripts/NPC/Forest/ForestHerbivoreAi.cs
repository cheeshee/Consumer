using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestHerbivoreAi : PacingNpcAi
{
    private float safeDistance; // distance that herbivore goes to before returning to path

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // THESE ARE CUSTOM
        charType = (int)PlayerProperties.CharacterType.ANIMAL;
        speed = 70f;
        safeDistance = 5f; // must be larger than detection on herbivore
        // TODO: Health and other variables
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    // NPC AI overrides
    public override void CheckOutPlayer(){
        // leave regardless, spooked
        reachedEndOfPath = true;
        onPath = false;
    }

    protected override void ReactToPlayer(){
        direction = (playerScript.GetPosition() - npcRb.position).normalized;
        velocityX = -1.2f * direction.x * speed * Time.deltaTime;
        npcRb.velocity = new Vector2(velocityX, npcRb.velocity.y);
    }

    protected override void WalkPath(){
        if ( Vector2.Distance(npcRb.position, playerScript.GetPosition())> safeDistance){
            base.WalkPath();            
        } else {
            // npcRb.velocity = new Vector2(0f, npcRb.velocity.y);
        }// else rest
    }
}

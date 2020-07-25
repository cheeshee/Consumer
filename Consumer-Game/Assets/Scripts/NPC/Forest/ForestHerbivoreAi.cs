using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestHerbivoreAi : PacingNpcAi
{
    [SerializeField]
    private float safeDistance; // distance that herbivore goes to before returning to path
    private float restTime; // time to wait at safe distance before resuming path
    private float timeDetected;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // THESE ARE CUSTOM
        charType = (int)PlayerProperties.CharacterType.ANIMAL;
        speed = 70f;
        safeDistance = 5f; // must be larger than detection on herbivore
        restTime = 3f; // time should be longer than needed to walk to safe distance
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
        timeDetected = Time.time;
    }

    protected override void ReactToPlayer(){
        direction = (playerScript.GetPosition() - npcRb.position).normalized;
        velocityX = -1.2f * direction.x * speed * Time.deltaTime;
        npcRb.velocity = new Vector2(velocityX, npcRb.velocity.y);
    }

    protected override void WalkPath(){
        if ( Vector2.Distance(npcRb.position, playerScript.GetPosition())> safeDistance && Time.time - timeDetected > restTime){
            base.WalkPath();            
        } else{
            // npcRb.velocity = new Vector2(0f, npcRb.velocity.y);
        }// else rest
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestHerbivoreAi : PacingNpcAi
{
    [SerializeField]
    protected float safeDistance; // distance that herbivore goes to before returning to path
    [SerializeField]
    protected float restTime; // time to wait at safe distance before resuming path
    [SerializeField]
    protected float escapeSpeedMultiplier; // speed multiplier for npc while escaping

    protected float timeDetected;
    protected bool escaping;  // currently running away

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // THESE ARE CUSTOM
        charType = (int)PlayerProperties.CharacterType.ANIMAL;
        speed = 70f;
        safeDistance = 5f; // must be larger than detection on herbivore
        restTime = 3f; // time should be longer than needed to walk to safe distance
        escapeSpeedMultiplier = 1.2f;
        npcController = new ForestHerbivoreController(gameObject);
        // TODO: Health and other variables
        health = 100;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    // NPC AI overrides
    public override void CheckOutPlayer(){
        // leave regardless, spooked
        escaping = true;
        reachedEndOfPath = true;
        onPath = false;
        timeDetected = Time.time;
    }

    protected override void ReactToPlayer(){
        direction = (playerScript.GetPosition() - npcRb.position).normalized;
        velocityX = -escapeSpeedMultiplier * direction.x * speed * Time.deltaTime;
        npcRb.velocity = new Vector2(velocityX, npcRb.velocity.y);
    }

    protected override void WalkPath(){
        if (escaping && Vector2.Distance(npcRb.position, playerScript.GetPosition()) <= safeDistance){
            // keep walking to safe distance
            timeDetected = Time.time;
            ReactToPlayer();
        } else if (Time.time - timeDetected < restTime) {
            escaping = false;
            npcRb.velocity = new Vector2(0f, npcRb.velocity.y);
        } else {
            base.WalkPath();            
        } 
    }
}

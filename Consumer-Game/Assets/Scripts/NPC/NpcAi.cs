using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NpcAi : MonoBehaviour
{
    // TODO: add variables for health and attack damage, etc.
    
    
    // variables for checking how environment interacts with player
    protected int charType;

    protected Rigidbody2D npcRb;

    protected bool onPath;
    protected SpriteRenderer npcGraphics;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        npcRb = GetComponent<Rigidbody2D>();
        npcGraphics = GetComponent<SpriteRenderer>();
        onPath = true;
    }

    

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        UpdateGraphics();
    }


    private void UpdateGraphics(){        
        if (npcRb.velocity.x >= 0.01f){
            npcGraphics.flipX = false;
        } else if (npcRb.velocity.x < -0.01f){
            npcGraphics.flipX = true;
        }
    }
    
    protected virtual void ReactToPlayer(){
        // what happens if npc reacts to player
    }

    public virtual void CheckOutPlayer(GameObject player){
        // decide how to treat player
    }
}

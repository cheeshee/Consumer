using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NpcAi : MonoBehaviour
{
    // TODO: add variables for health and attack damage, etc.
    

    protected Rigidbody2D rb;

    protected SpriteRenderer npcGraphics;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        npcGraphics = GetComponent<SpriteRenderer>();
    }

    

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if (rb.velocity.x >= 0.01f){
            npcGraphics.flipX = false;
        } else if (rb.velocity.x < -0.01f){
            npcGraphics.flipX = true;
        }
    }
}

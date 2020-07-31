using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NpcAi : MonoBehaviour, HealthInterface
{
    // TODO: add variables for health and attack damage, etc.
    
    
    // variables for checking how environment interacts with player
    protected int charType;

    protected Rigidbody2D npcRb;

    protected bool onPath;
    protected bool isDead;
    protected SpriteRenderer npcGraphics;
    protected PlayerManager playerScript;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        npcRb = GetComponent<Rigidbody2D>();
        npcGraphics = GetComponent<SpriteRenderer>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        onPath = true;
    }

    protected virtual void Update(){

    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        UpdateGraphics();
    }


    protected virtual void UpdateGraphics(){        
        if (npcRb.velocity.x >= 0.01f){
            npcGraphics.flipX = false;
        } else if (npcRb.velocity.x < -0.01f){
            npcGraphics.flipX = true;
        }
    }

    public virtual void CheckOutPlayer(){
        // decide how to treat player
    }

    public virtual void PlayerNoLongerDetected(){
        // player is out of range
        onPath = true;
    }
    
    protected virtual void ReactToPlayer(){
        // what happens if npc reacts to player
    }





    //Heatlh
    
    [HideInInspector] public int health { get; set; }
    public int maxHealth  { get; set; }
    public virtual void InitializeHealth()
    {
        
        maxHealth = 100;
        health = maxHealth;
    }

    public virtual void ApplyDamage(int points)
    {
        health = Mathf.Clamp(health - points, 0, maxHealth);
        if (health <= 0){
            onDeath();
        }

    }

    public virtual void onDeath(){
        isDead = true;
    }

    public virtual void onConsume(){
        //Delete Body
        //Place correct 
    }


}

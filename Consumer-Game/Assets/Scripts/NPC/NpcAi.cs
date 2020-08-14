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
    protected Collider2D npcCollider;
    // indicate consumable
    protected GameObject indicator;
    protected Animator indicatorAnimator;

    protected bool onPath;
    [SerializeField]
    protected bool isDead;

    protected SpriteRenderer npcGraphics;
    protected PlayerManager playerScript;

    protected PlayerController npcController;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        npcRb = GetComponent<Rigidbody2D>();
        npcCollider = GetComponent<Collider2D>();
        npcGraphics = GetComponent<SpriteRenderer>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        // indicate consumable
        indicator = transform.GetChild(1).gameObject;
        indicatorAnimator = indicator.GetComponent<Animator>();
        indicator.SetActive(false);

        onPath = true;
        isDead = false;
        // TODO: instantiate an appropriate npcController 
        npcController = new VillagerController(gameObject);
        InitializeHealth();
    }

    protected virtual void Update(){

    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        UpdateGraphics();
        DetectConsumable();
        // test purpose
        // onDeath();
    }


    protected virtual void UpdateGraphics(){        
        if (npcRb.velocity.x >= 0.01f){
            npcGraphics.flipX = false;
        } else if (npcRb.velocity.x < -0.01f){
            npcGraphics.flipX = true;
        }
    }

    protected virtual void DetectConsumable(){
        if (isDead && playerScript.CheckClosestInteraction(npcCollider)){
            playerScript.SetCanConsume(true);
            indicatorAnimator.SetBool("inRange", true);
        } else {
            indicatorAnimator.SetBool("inRange", false);
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
        Debug.Log("NPC Took " + points + " damage, I have " + health + "health");
        if (health <= 0){
            onDeath();
        }

    }

    public virtual void onDeath(){
        isDead = true;
        // set interaction object to inactive
        transform.GetChild(0).gameObject.SetActive(false);
        npcRb.constraints = RigidbodyConstraints2D.FreezeAll;
        npcCollider.isTrigger = true;
        gameObject.layer = (int) Layers.Interaction;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == (int) Layers.Player && isDead){
            indicator.SetActive(true);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == (int) Layers.Player){
            indicator.SetActive(false);
            playerScript.LeaveClosestInteraction(npcCollider);
            playerScript.SetCanConsume(false);
        }
    }

    // for saving into player manager
    public PlayerController GetController(){
        return npcController;
    }
    public Sprite GetSprite(){
        return npcGraphics.sprite;
    }
    public Rigidbody2D GetRigidbody(){
        return npcRb;
    }

}

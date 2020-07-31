﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Array of possible controllers for player
    private PlayerController[] characterSlots = new PlayerController[8];
    private int currCharacter = 0;
    private Rigidbody2D playerRb;

    // checking interactions for raycast
    private Collider2D closestInteraction;
    private float closestDist;
    private float distToColliderLeft;
    private float distToColliderRight;
    private int interactionLayerMask;

    private PhysicsMaterial2D fullFriction;
    private PhysicsMaterial2D noFriction;

    [SerializeField] LayerMask collisionLayerMask;
    
    // detect consume variables
    protected bool consumable;
    protected float startConsume;

    protected bool inSlotSelection;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody2D>();

        // initialize raycast variables
        distToColliderLeft = Mathf.Infinity;
        distToColliderRight = Mathf.Infinity;

        fullFriction = Resources.Load<PhysicsMaterial2D>("PhysicsMaterial/" + "FullFrictionMaterial");
        noFriction = Resources.Load<PhysicsMaterial2D>("PhysicsMaterial/" + "NoFrictionMaterial");
        Debug.Assert(fullFriction != null);
        Debug.Assert(noFriction != null);


        // testing stuff
        characterSlots[0] = new InitialController(gameObject);
        characterSlots[0].OnSwitch(gameObject);
        characterSlots[1] = new VillagerController(gameObject);
        
        closestDist = Mathf.Infinity;


    }

    // Update is called once per frame
    void Update()
    {
        if (inSlotSelection){
            SaveController();
        } else {
            DetectMove();
            DetectJump();
            DetectAttack();
            DetectConsume();
            DetectShapeShift();
        }

    }

    private void FixedUpdate() {
        characterSlots[currCharacter].FixedUpdate();

        // raycast for interactions
        // FindClosestInteraction();
        
    }



    private void DetectJump()
    {

        characterSlots[currCharacter].Jump();

    }

    private void DetectMove()
    {

        characterSlots[currCharacter].Move();

    }

    
    private void DetectAttack()
    {

        characterSlots[currCharacter].Attack();

    }


    private void DetectConsume()
    {   

        //TODO
        //Need another closest interaction
        if (consumable){
            if (Input.GetButtonDown("Interact")){
                startConsume = Time.time;
            } else if (Input.GetButton("Interact") && (Time.time - startConsume) > 1f){
                // start consume
                inSlotSelection = true;
                Debug.Log("display some UI here");
            }
        }

    }

    private void DetectShapeShift()
    {
        int slot = GetSlotSelected();
        currCharacter = slot < 0? currCharacter : slot;
        characterSlots[currCharacter].OnSwitch(gameObject);
    }

    private void SaveController(){

        int saveSlot = GetSlotSelected();
        Debug.Log("choose a slot");
        if (saveSlot >= 0){
            Debug.Log("saving the new controller somehow");
            Debug.Log("saveSlot = " + saveSlot);
            NpcAi deadNPC;
            deadNPC = closestInteraction.gameObject.GetComponent<NpcAi>();
            characterSlots[saveSlot] = deadNPC.GetController();
            Debug.Log("newly saved: " + characterSlots[saveSlot]);
            //Delete Body
            closestInteraction.gameObject.SetActive(false);
            Debug.Log("newly saved: " + characterSlots[saveSlot]);
            LeaveClosestInteraction(closestInteraction);
            consumable = false;
            inSlotSelection = false;
        }


    }

    private int GetSlotSelected(){
        int slot = -1;
        if(Input.GetButtonDown(InputProperties.FIRST))
        {
            slot = (int)InputProperties.Slots.FIRST;
        } 
        else if(Input.GetButtonDown(InputProperties.SECOND))
        {
            slot = (int)InputProperties.Slots.SECOND;
        } 
        else if(Input.GetButtonDown(InputProperties.THIRD))
        {
            slot = (int)InputProperties.Slots.THIRD;
        } 
        else if(Input.GetButtonDown(InputProperties.FOURTH))
        {
            slot = (int)InputProperties.Slots.FOURTH;
        } 
        else if(Input.GetButtonDown(InputProperties.FIFTH))
        {
            slot = (int)InputProperties.Slots.FIFTH;
        } 
        else if(Input.GetButtonDown(InputProperties.SIXTH))
        {
            slot = (int)InputProperties.Slots.SIXTH;
        } 
        else if(Input.GetButtonDown(InputProperties.EIGHTH))
        {
            slot = (int)InputProperties.Slots.EIGHTH;
        } 
        return slot;
    }

    public Vector2 GetPosition(){
        return gameObject.transform.position;
    }

    public Vector2 GetLocalScale(){
        return gameObject.transform.localScale;
    }

    public PhysicsMaterial2D GetFriction (bool friction){
        if (friction){
            return fullFriction;
        
        }
        else{
            return noFriction;
        }
    }

    public LayerMask GetPlayerLayerMask (){
        return collisionLayerMask;
    }

    public int GetCharType(){
        return characterSlots[currCharacter].GetCharType();
    }

    public void FlipHorizontal(bool facingRight){
        if (facingRight){
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else{
            transform.localScale = new Vector3(-1f, 1f, 1f);

        }
    }

    public bool CheckClosestInteraction(Collider2D range){
        if (closestInteraction != range){
            float distTo = Mathf.Abs(range.transform.position.x - transform.position.x);
            if(distTo < closestDist){
                closestDist = distTo;
                closestInteraction = range;
                return true;
            } else {
                return false;
            }
        } else {
            return true;
        }
    }

    public void LeaveClosestInteraction(Collider2D range){
        if(closestInteraction == range){
            closestDist = Mathf.Infinity;
            closestInteraction = null;
        }
    }

    public void CanConsume(bool consume){
        consumable = consume;
    }

    
}

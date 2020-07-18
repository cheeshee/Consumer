using System.Collections;
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
    private Collider2D currClosest;
    private float distToColliderLeft;
    private float distToColliderRight;
    private int interactionLayerMask;
    RaycastHit2D hitLeft;
    RaycastHit2D hitRight;
    

    // Start is called before the first frame update
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody2D>();

        // initialize raycast variables
        distToColliderLeft = Mathf.Infinity;
        distToColliderRight = Mathf.Infinity;

        // testing stuff
        characterSlots[0] = new InitialController(gameObject);
        characterSlots[0].OnSwitch(gameObject);
        characterSlots[1] = new VillagerController(gameObject);
        


    }

    // Update is called once per frame
    void Update()
    {
        DetectMove();
        DetectJump();
        DetectShapeShift();
    }

    private void FixedUpdate() {
        characterSlots[currCharacter].FixedUpdate();

        // raycast for interactions
        FindClosestInteraction();
        
    }



    private void DetectJump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            characterSlots[currCharacter].Jump();
        }
    }

    private void DetectMove()
    {

        characterSlots[currCharacter].Move();

    }

    private void DetectShapeShift()
    {
        if(Input.GetButtonDown(InputProperties.FIRST))
        {
            currCharacter = (int)InputProperties.Slots.FIRST;
        } 
        else if(Input.GetButtonDown(InputProperties.SECOND))
        {
            currCharacter = (int)InputProperties.Slots.SECOND;
        } 
        else if(Input.GetButtonDown(InputProperties.THIRD))
        {
            currCharacter = (int)InputProperties.Slots.THIRD;
        } 
        else if(Input.GetButtonDown(InputProperties.FOURTH))
        {
            currCharacter = (int)InputProperties.Slots.FOURTH;
        } 
        else if(Input.GetButtonDown(InputProperties.FIFTH))
        {
            currCharacter = (int)InputProperties.Slots.FIFTH;
        } 
        else if(Input.GetButtonDown(InputProperties.SIXTH))
        {
            currCharacter = (int)InputProperties.Slots.SIXTH;
        } 
        else if(Input.GetButtonDown(InputProperties.EIGHTH))
        {
            currCharacter = (int)InputProperties.Slots.EIGHTH;
        } 
        characterSlots[currCharacter].OnSwitch(gameObject);
    }

    private void FindClosestInteraction(){
        // change layerMask as necessary TODO
        interactionLayerMask = 1<<8 | 1<<9;
        interactionLayerMask = ~interactionLayerMask;
        hitLeft = Physics2D.Raycast(transform.position, -Vector2.right, 5f, interactionLayerMask);
        hitRight = Physics2D.Raycast(transform.position, Vector2.right, 5f, interactionLayerMask);

        // check left
        if (hitLeft.collider != null) {
            distToColliderLeft = Mathf.Abs(hitLeft.centroid.x - transform.position.x); 
        } else {
            distToColliderLeft = Mathf.Infinity;
        }
        // check right
        if (hitRight.collider != null) {
            distToColliderRight = Mathf.Abs(hitRight.centroid.x - transform.position.x); 
        } else {
            distToColliderRight = Mathf.Infinity;
        }
        
        if (closestInteraction != null) {
            
            closestInteraction.gameObject.GetComponent<DialogueController>().setClosest(false);
        }

        Debug.Log(distToColliderLeft + " vs " + distToColliderRight);
        if(distToColliderLeft < distToColliderRight){
            closestInteraction = hitLeft.collider;
            closestInteraction.gameObject.GetComponent<DialogueController>().setClosest(true);
        } else {
            closestInteraction = hitRight.collider;
            Debug.Log(closestInteraction + " is closest");
            closestInteraction.gameObject.GetComponent<DialogueController>().setClosest(true);

        }
    }
}

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

    private PhysicsMaterial2D fullFriction;
    private PhysicsMaterial2D noFriction;

    [SerializeField] LayerMask collisionLayerMask;
    RaycastHit2D hitLeft;
    RaycastHit2D hitRight;
    

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

        characterSlots[currCharacter].Jump();

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

    public Vector2 GetPosition(){
        return gameObject.transform.position;
    }

    public Vector2 GetLocalScale(){
        return gameObject.transform.localScale;
    }

    private void FindClosestInteraction(){
        // change layerMask as necessary TODO
        interactionLayerMask = 1<<10;
        hitLeft = Physics2D.Raycast(transform.position, -Vector2.right, 2f, interactionLayerMask);
        hitRight = Physics2D.Raycast(transform.position, Vector2.right, 2f, interactionLayerMask);

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

        if (hitRight.collider != null || hitLeft.collider != null) {
            if(distToColliderLeft < distToColliderRight){
                closestInteraction = hitLeft.collider;
                closestInteraction.gameObject.GetComponent<DialogueController>().setClosest(true);
            } else {
                closestInteraction = hitRight.collider;
                closestInteraction.gameObject.GetComponent<DialogueController>().setClosest(true);

            }
        }
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

}

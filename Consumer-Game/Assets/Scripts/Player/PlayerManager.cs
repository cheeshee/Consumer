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
    private float closestDist;
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
        
        closestDist = Mathf.Infinity;


    }

    // Update is called once per frame
    void Update()
    {
        DetectMove();
        DetectJump();
        DetectAttack();
        DetectConsume();
        DetectShapeShift();
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

}

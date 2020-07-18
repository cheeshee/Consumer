using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Array of possible controllers for player
    private PlayerController[] characterSlots = new PlayerController[8];
    private int currCharacter = 0;
    private Rigidbody2D playerRb;
    

    // Start is called before the first frame update
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody2D>();
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

    public Vector2 GetPosition(){
        return gameObject.transform.position;
    }

    public Vector2 GetLocalScale(){
        return gameObject.transform.localScale;
    }
}

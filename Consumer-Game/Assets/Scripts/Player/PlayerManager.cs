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
        characterSlots[0] = new InitialController(gameObject);
        characterSlots[0].OnSwitch(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        DetectMove();
        DetectJump();
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
        if(Input.GetButton("Move")){
            characterSlots[currCharacter].Move();
        }
    }
}

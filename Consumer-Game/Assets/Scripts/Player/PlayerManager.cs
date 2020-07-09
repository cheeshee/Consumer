using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Array of possible controllers for player
    private PlayerController[] characterSlots = new PlayerController[8];
    private int currCharacter = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        characterSlots[0] = new InitialController(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        DetectJump();
    }

    private void DetectJump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            characterSlots[currCharacter].Jump();
        }
    }
}

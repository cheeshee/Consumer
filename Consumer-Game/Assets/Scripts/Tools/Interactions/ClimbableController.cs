using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbableController : InteractionController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(inRange && playerScript.CheckClosestInteraction(playerDetectionCollider)){//closestToPlayer){
            EnterInteraction();
        } 
        // else if (inInteraction && Input.GetButtonDown("Interact")){
        //     playerScript.StopClimbing();
        // }
    }

    protected override void EnterInteraction(){
        if (Input.GetButtonDown("Interact")){
            // Setting UI components
            inInteraction = true;       
            // do stuff to start interaction    
            playerScript.Climbing(); 
            inRange = false;
            indicator.SetActive(false);
            // playerScript.LeaveClosestInteraction(playerDetectionCollider);
        }
    }
}

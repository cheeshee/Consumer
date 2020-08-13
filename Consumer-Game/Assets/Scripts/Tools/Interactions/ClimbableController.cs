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

    protected override void OnTriggerEnter2D(Collider2D other){
        // nothing, replace with onTriggerStay below
    }

    protected override void OnTriggerExit2D(Collider2D other){
        base.OnTriggerExit2D(other);
        inInteraction = false;
    }

    private void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.layer == (int) Layers.Player && playerScript.CanClimb() && !inInteraction){
            playerRb = other.gameObject.GetComponent<Rigidbody2D>();
            inRange = true;
            indicator.SetActive(true);
        } else {
            inRange = false;
            indicator.SetActive(false);
        }
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

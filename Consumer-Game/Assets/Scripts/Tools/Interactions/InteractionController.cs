using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    protected Collider2D playerDetectionCollider;
    protected GameObject indicator;
    protected Animator indicatorAnimator;
    protected bool inRange;
    protected bool inInteraction;
    protected Rigidbody2D playerRb;
    protected PlayerManager playerScript;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerDetectionCollider = GetComponent<Collider2D>();
        indicator = transform.GetChild(0).gameObject;
        indicatorAnimator = indicator.GetComponent<Animator>();
        indicator.SetActive(false);
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();

        inRange = false;
        inInteraction = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(inRange && playerScript.CheckClosestInteraction(playerDetectionCollider)){//closestToPlayer){
            indicatorAnimator.SetBool("inRange", true);
            EnterInteraction();
        } else {
            indicatorAnimator.SetBool("inRange", false);
        }
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.layer == (int) Layers.Player){
            playerRb = other.gameObject.GetComponent<Rigidbody2D>();
            inRange = true;
            indicator.SetActive(true);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == (int) Layers.Player){
            inRange = false;
            indicator.SetActive(false);
            playerScript.LeaveClosestInteraction(playerDetectionCollider);
        }
    }

    protected virtual void EnterInteraction(){
        if (Input.GetButtonDown("Interact")){
            // Setting UI components
            inInteraction = true;       
            // do stuff to start interaction     
        }
        
    }
}

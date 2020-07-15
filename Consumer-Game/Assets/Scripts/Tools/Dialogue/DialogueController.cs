using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    Collider2D playerDetectionCollider;
    bool inTalkingRange;
    // Start is called before the first frame update
    void Start()
    {
        playerDetectionCollider = GetComponent<Collider2D>();
        inTalkingRange = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.layer == (int) Layers.Player){
            inTalkingRange = true;
            Debug.Log("in talking range");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == (int) Layers.Player){
            inTalkingRange = false;
            Debug.Log("exiting talking range");
        }
    }
}

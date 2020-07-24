using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private Collider2D playerDetectionCollider;
    private bool inRange;
    private NpcAi npcAi;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        playerDetectionCollider = GetComponent<Collider2D>();
        inRange = false;
        npcAi = GetComponentInParent<NpcAi>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange){
            npcAi.CheckOutPlayer(player);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
            Debug.Log("checking out player");
        if (other.gameObject.layer == (int) Layers.Player){
            player = other.gameObject;
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == (int) Layers.Player){
            inRange = false;
            player = null;
        }
    }    
}

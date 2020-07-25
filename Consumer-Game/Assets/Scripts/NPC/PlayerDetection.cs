using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private Collider2D playerDetectionCollider;
    private NpcAi npcAi;

    // Start is called before the first frame update
    void Start()
    {
        playerDetectionCollider = GetComponent<Collider2D>();
        npcAi = GetComponentInParent<NpcAi>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other) {
            Debug.Log("checking out player");
        if (other.gameObject.layer == (int) Layers.Player){
            npcAi.CheckOutPlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == (int) Layers.Player){
            npcAi.ReturnToPath();
        }
    }    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class DialogueController : MonoBehaviour
{
    protected Collider2D playerDetectionCollider;
    protected GameObject indicator;
    protected Animator indicatorAnimator;
    protected bool inRange;
    protected bool inInteraction;
    protected Rigidbody2D playerRb;
    protected GameObject textDisplayCanvas;

    // protected bool closestToPlayer;
    protected PlayerManager playerScript;


    [SerializeField]
    protected TextAsset textFile;
    protected JSONObject textJSON;

    // dialgoue progress variables
    protected int stage;              // mark stages or event triggers
    protected float currentSectionID;   // mark conversation to be held at current stage, s.x where s is the stage number, 
    //currently max 10 conversations each stage change above as needed
   
    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerDetectionCollider = GetComponent<Collider2D>();
        indicator = transform.GetChild(0).gameObject;
        indicatorAnimator = indicator.GetComponent<Animator>();
        indicator.SetActive(false);
        textDisplayCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        inRange = false;
        inInteraction = false; 

        // TODO
        // grab these values whereever they're saved
        stage = 0;
        currentSectionID = 0;

        // load the dialogue text
        loadDialogueJSON();
   
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(inRange && playerScript.CheckClosestInteraction(playerDetectionCollider)){//closestToPlayer){
            indicatorAnimator.SetBool("inRange", true);
            EnterDialogue();
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

    protected virtual void EnterDialogue(){
        if (Input.GetButtonDown("Interact") && !textDisplayCanvas.activeSelf){
            // Setting UI components
            inInteraction = true;            
            textDisplayCanvas.SetActive(true);
            // Pause player controls TODO
            playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
            // Pass lines to be displayed
            textDisplayCanvas.GetComponent<DialogueDisplayController>().FeedLines(textJSON["stages"][stage][(int)((currentSectionID - stage) * 10)].AsArray);
            // update stage and currentConvoID TODO
        }
        if (textDisplayCanvas.activeSelf){            
            playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
            indicator.SetActive(false);
        } else {
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
            indicator.SetActive(true);
        }
    }

    protected virtual void createSampleJSON(){
         //test // Testing
        JSONObject yeet = new JSONObject();
        // yeet.Add("currentStage", 0);
        // yeet.Add("currentConvoID", 0.0);

        JSONArray conversations = new JSONArray();
        JSONArray testlines = new JSONArray();
        JSONArray Singlelines = new JSONArray();
        Singlelines.Add("speaker", "anon");
        Singlelines.Add("line", "i literally wanna die");
        testlines.Add(Singlelines);
        Singlelines = new JSONArray();
        Singlelines.Add("speaker", "anon2");
        Singlelines.Add("line", "very cool");
        testlines.Add(Singlelines);
        conversations.Add(testlines);
        JSONArray stages = new JSONArray();
        stages.Add(conversations);
        stages.Add(conversations);
        
        yeet.Add("stages", stages);
        Debug.Log(yeet.ToString());
        string path = Application.dataPath + "/TextFiles/DialogueTest.json";
        File.WriteAllText(path, yeet.ToString());
    }

    protected virtual void loadDialogueJSON(){
        textJSON = (JSONObject) JSON.Parse(textFile.ToString());
        // will have to change and check these as we go, possibly skip to next stage
        // TODO
        // stage = textJSON["currentStage"];
        // currentConvoID = textJSON["currentConvoID"];    
    }

}
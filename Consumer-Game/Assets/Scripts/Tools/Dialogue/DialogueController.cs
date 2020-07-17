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
    private GameObject dialogueCanvas;

    protected bool closestToPlayer;

    [SerializeField]
    protected TextAsset textFile;
    protected JSONObject textJSON;

    // dialgoue progress variables
    protected int stage;              // mark stages or event triggers
    private float currentConvoID;   // mark conversation to be held at current stage, s.x where s is the stage number, 
    //currently max 10 conversations each stage change above as needed
   
    // Start is called before the first frame update
    void Start()
    {
        playerDetectionCollider = GetComponent<Collider2D>();
        indicator = transform.GetChild(0).gameObject;
        // indicatorAnimator = indicator.GetComponent<Animator>();
        indicator.SetActive(false);
        dialogueCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
        dialogueCanvas.SetActive(false);
        inRange = false;
        inInteraction = false; 
        closestToPlayer = false;  

        stage = 0;
        currentConvoID = 0;

        // load the dialogue text
        loadDialogueJSON();
   
    }

    // Update is called once per frame
    void Update()
    {
        if(inRange && closestToPlayer){
            EnterDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.layer == (int) Layers.Player){
            Debug.Log("in talking range");
            playerRb = other.gameObject.GetComponent<Rigidbody2D>();
            inRange = true;
            indicator.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == (int) Layers.Player){
            Debug.Log("exiting talking range");
            inRange = false;
            indicator.SetActive(false);
        }
    }

    private void EnterDialogue(){
        if (Input.GetButtonDown("Interact") && !dialogueCanvas.activeSelf){
            Debug.Log("Entering Dialogue");
            // Setting UI components
            inInteraction = true;            
            dialogueCanvas.SetActive(true);
            // Pause player controls TODO
            playerRb.bodyType = RigidbodyType2D.Static;
            // Pass lines to be displayed
            dialogueCanvas.GetComponent<DialogueDisplayController>().FeedLines(textJSON["stages"][stage][(int)(currentConvoID - stage) * 10].AsArray);
        }
        if (dialogueCanvas.activeSelf){            
            playerRb.bodyType = RigidbodyType2D.Static;
            indicator.SetActive(false);
        } else {
            playerRb.bodyType = RigidbodyType2D.Dynamic;
            indicator.SetActive(true);
        }
    }

    private void createSampleJSON(){
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

    private void loadDialogueJSON(){
        textJSON = (JSONObject) JSON.Parse(textFile.ToString());
        // will have to change and check these as we go, possibly skip to next stage
        // TODO
        // stage = textJSON["currentStage"];
        // currentConvoID = textJSON["currentConvoID"];    
    }

    public void setClosest(bool closest){
        closestToPlayer = closest;
        // indicatorAnimator.SetBool("inRange", closest);
    }

}
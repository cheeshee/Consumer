using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class DialogueController : MonoBehaviour
{
    private Collider2D playerDetectionCollider;
    private GameObject indicator;
    private GameObject dialogueCanvas;
    bool inTalkingRange;
    bool inDialogue;

    [SerializeField]
    private TextAsset textFile;
    private JSONObject textJSON;

    // dialgoue progress variables
    private int stage;              // mark stages or event triggers
    private float currentConvoID;   // mark conversation to be held at current stage, s.x where s is the stage number, 
    //currently max 10 conversations each stage change above as needed
   



    // Start is called before the first frame update
    void Start()
    {
        playerDetectionCollider = GetComponent<Collider2D>();
        indicator = transform.GetChild(0).gameObject;
        indicator.SetActive(false);
        dialogueCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
        dialogueCanvas.SetActive(false);
        inTalkingRange = false;
        inDialogue = false;   

        // load the dialogue text
        loadDialogueJSON();
   
    }

    // Update is called once per frame
    void Update()
    {
        if(inTalkingRange){
            EnterDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.layer == (int) Layers.Player){
            Debug.Log("in talking range");
            inTalkingRange = true;
            indicator.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == (int) Layers.Player){
            Debug.Log("exiting talking range");
            inTalkingRange = false;
            indicator.SetActive(false);
        }
    }

    private void EnterDialogue(){
        if (Input.GetButtonDown("Interact")){
            Debug.Log("Entering Dialogue");
            // Setting UI components
            inDialogue = true;            
            dialogueCanvas.SetActive(true);
            // Pause player controls TODO
            
            // Pass lines to be displayed
            dialogueCanvas.GetComponent<DialogueDisplayController>().FeedLines(textJSON["stages"][stage][(int)(currentConvoID - stage) * 10].AsArray);
        }
        indicator.SetActive(!dialogueCanvas.activeSelf);
    }

    private void createSampleJSON(){
         //test // Testing
        JSONObject yeet = new JSONObject();
        yeet.Add("currentStage", 0);
        yeet.Add("currentConvoID", 0.0);

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
        string path = Application.dataPath + "/Dialogue/test.json";
        File.WriteAllText(path, yeet.ToString());
    }
    private void loadDialogueJSON(){
        textJSON = (JSONObject) JSON.Parse(textFile.ToString());
        // will have to change and check these as we go, possibly skip to next stage
        // TODO
        stage = textJSON["currentStage"];
        currentConvoID = textJSON["currentConvoID"];    
    }
}

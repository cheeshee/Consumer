using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class DialogueController : InteractionController
{
    protected GameObject textDisplayCanvas;

    // protected bool closestToPlayer;


    [SerializeField]
    protected TextAsset textFile;
    protected JSONObject textJSON;

    // dialgoue progress variables
    protected int stage;              // mark stages or event triggers
    protected float currentSectionID;   // mark conversation to be held at current stage, s.x where s is the stage number, 
    //currently max 10 conversations each stage change above as needed
   
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        textDisplayCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
        // TODO
        // grab these values whereever they're saved
        stage = 0;
        currentSectionID = 0;

        // load the dialogue text
        loadDialogueJSON();
   
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void EnterInteraction(){
        if (Input.GetButtonDown("Interact") && !textDisplayCanvas.activeSelf){
            // Setting UI components
            inInteraction = true;            
            textDisplayCanvas.SetActive(true);
            // Pause player controls TODO
            playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
            // Pass lines to be displayed
            textDisplayCanvas.GetComponent<DialogueDisplayController>().FeedLines(textJSON["stages"][stage][(int)(currentSectionID - stage) * 10].AsArray);
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
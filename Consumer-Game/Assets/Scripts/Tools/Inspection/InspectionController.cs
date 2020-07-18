using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class InspectionController : DialogueController
{


    // dialgoue progress variables
    //float currentSectionID;   // mark description to be held at current stage, s.x where s is the stage number, 
    //currently max 10 description each stage change above as needed

    // Start is called before the first frame update
    protected override void Start()
    {   
        // consider refactoring this more to save redundant code
        playerDetectionCollider = GetComponent<Collider2D>();
        indicator = transform.GetChild(0).gameObject;
        indicatorAnimator = indicator.GetComponent<Animator>();
        indicator.SetActive(false);
        textDisplayCanvas = GameObject.FindGameObjectWithTag("InspectionCanvas");
        // inspectionCanvas.SetActive(false);
        inRange = false;
        inInteraction = false;   
        closestToPlayer = false;  

        // TODO
        // grab these values whereever they're saved
        stage = 0;
        currentSectionID = 0;

        // load the dialogue text
        loadDialogueJSON();
    }


    protected override void EnterDialogue(){
        if (Input.GetButtonDown("Interact") && !textDisplayCanvas.activeSelf){
            Debug.Log("Entering Inspection");
            // Setting UI components
            inInteraction = true;            
            textDisplayCanvas.SetActive(true);
            // Pause player controls TODO
            playerRb.bodyType = RigidbodyType2D.Static;
            // Pass lines to be displayed
            textDisplayCanvas.GetComponent<InspectionDisplayController>().FeedLines(textJSON["stages"][stage][(int)(currentSectionID - stage) * 10].AsArray);
            // update stage and currentConvoID TODO
            
        }
        if (textDisplayCanvas.activeSelf){            
            playerRb.bodyType = RigidbodyType2D.Static;
            indicator.SetActive(false);
        } else {
            playerRb.bodyType = RigidbodyType2D.Dynamic;
            indicator.SetActive(true);
        }
    }

    protected override void createSampleJSON(){
         //test // Testing
        JSONObject yeet = new JSONObject();
        yeet.Add("type", "static");

        JSONArray description = new JSONArray();
        JSONArray testlines = new JSONArray();
        JSONArray Singlelines = new JSONArray();
        Singlelines.Add("object", "thing");
        Singlelines.Add("sentence", "this is a thing");
        testlines.Add(Singlelines);
        Singlelines = new JSONArray();
        Singlelines.Add("object", "thing");
        Singlelines.Add("sentence", "maybe it does something");
        testlines.Add(Singlelines);
        description.Add(testlines);
        JSONArray stages = new JSONArray();
        stages.Add(description);
        stages.Add(description);
        
        yeet.Add("stages", stages);
        Debug.Log(yeet.ToString());
        string path = Application.dataPath + "/TextFiles/InspectionTest.json";
        File.WriteAllText(path, yeet.ToString());
    }
    
    protected override void loadDialogueJSON(){
        textJSON = (JSONObject) JSON.Parse(textFile.ToString());
        // will have to change and check these as we go, possibly skip to next stage
        // TODO
        // these values are no longer saved here, need to make new progress files to hold
        if (textJSON["type"] == "static"){
            stage = 0;
            currentSectionID = 0;
        } else {
            // check stage and current description
        } 
    }
}

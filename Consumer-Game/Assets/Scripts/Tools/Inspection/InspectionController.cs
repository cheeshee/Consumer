using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class InspectionController : MonoBehaviour
{
    private Collider2D playerDetectionCollider;
    private GameObject indicator;
    private GameObject inspectionCanvas;
    private bool inInspectionRange;
    private bool inInspection;
    private Rigidbody2D playerRb;

    [SerializeField]
    private TextAsset textFile;
    private JSONObject textJSON;

    // dialgoue progress variables
    private int stage;              // mark stages or event triggers
    private float currentDescriptionID;   // mark conversation to be held at current stage, s.x where s is the stage number, 
    //currently max 10 conversations each stage change above as needed

    // Start is called before the first frame update
    void Start()
    {
        playerDetectionCollider = GetComponent<Collider2D>();
        indicator = transform.GetChild(0).gameObject;
        indicator.SetActive(false);
        inspectionCanvas = GameObject.FindGameObjectWithTag("InspectionCanvas");
        inspectionCanvas.SetActive(false);
        inInspectionRange = false;
        inInspection = false;   

        // TODO
        // grab these values whereever they're saved
        stage = 0;
        currentDescriptionID = 0;

        // load the dialogue text
        loadDialogueJSON();
    }

    // Update is called once per frame
    void Update()
    {
        if(inInspectionRange){
            EnterDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.layer == (int) Layers.Player){
            Debug.Log("in inspection range");
            playerRb = other.gameObject.GetComponent<Rigidbody2D>();
            inInspectionRange = true;
            indicator.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == (int) Layers.Player){
            Debug.Log("exiting inspection range");
            inInspectionRange = false;
            indicator.SetActive(false);
        }
    }

    private void EnterDialogue(){
        if (Input.GetButtonDown("Interact") && !inspectionCanvas.activeSelf){
            Debug.Log("Entering Inspection");
            // Setting UI components
            inInspection = true;            
            inspectionCanvas.SetActive(true);
            // Pause player controls TODO
            playerRb.bodyType = RigidbodyType2D.Static;
            // Pass lines to be displayed
            inspectionCanvas.GetComponent<InspectionDisplayController>().FeedLines(textJSON["stages"][stage][(int)(currentDescriptionID - stage) * 10].AsArray);
            // update stage and currentConvoID TODO
            
        }
        if (inspectionCanvas.activeSelf){            
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
    private void loadDialogueJSON(){
        textJSON = (JSONObject) JSON.Parse(textFile.ToString());
        // will have to change and check these as we go, possibly skip to next stage
        // TODO
        // these values are no longer saved here, need to make new progress files to hold
        if (textJSON["type"] == "static"){
            stage = 0;
            currentDescriptionID = 0;
        } else {
            // check stage and current description
        } 
    }
}

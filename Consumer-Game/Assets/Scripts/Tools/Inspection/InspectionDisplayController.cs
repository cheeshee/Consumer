using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class InspectionDisplayController : MonoBehaviour
{
    private GameObject objectBox;
    private Text objectName;
    private GameObject textBox;
    private Text descriptionText;
    private JSONArray description;

    private enum Line{OBJECT_NAME, SENTENCE}
    private int sentenceIndex;         // index for sentence in conversation

    // Start is called before the first frame update
    void Start()
    {
        objectBox = transform.GetChild(0).gameObject;
        objectName = objectBox.GetComponentInChildren<Text>();
        textBox = transform.GetChild(1).gameObject;
        descriptionText = textBox.GetComponentInChildren<Text>();
        sentenceIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        DetectNextLine();
    }

    public int FeedLines(JSONArray lines){
        Debug.Log("displaying text");
        Debug.Log(lines);
        sentenceIndex = 0;
        description = lines;
        objectName.text = description[sentenceIndex][(int)Line.OBJECT_NAME];
        descriptionText.text = description[sentenceIndex][(int)Line.SENTENCE];
        
        return 0;
    }

    private void DetectNextLine(){
        if (Input.anyKeyDown){
            sentenceIndex++;
            if (sentenceIndex < description.Count) {
                objectName.text = description[sentenceIndex][(int)Line.OBJECT_NAME];
                descriptionText.text = description[sentenceIndex][(int)Line.SENTENCE];
            }
            else {
                gameObject.SetActive(false);
            }

        }
    }
}

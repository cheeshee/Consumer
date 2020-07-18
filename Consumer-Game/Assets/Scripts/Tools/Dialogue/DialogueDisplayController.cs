using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class DialogueDisplayController : MonoBehaviour
{
    protected GameObject headerBox;
    protected Text headerName;
    protected GameObject textBox;
    protected Text descriptionText;
    protected JSONArray description;

    protected enum Line{HEADER_NAME, SENTENCE}
    protected int sentenceIndex;         // index for sentence in conversation

    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        headerBox = transform.GetChild(0).gameObject;
        headerName = headerBox.GetComponentInChildren<Text>();
        textBox = transform.GetChild(1).gameObject;
        descriptionText = textBox.GetComponentInChildren<Text>();
        sentenceIndex = 0;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (description == null){
            gameObject.SetActive(false);
        } else {
            DetectNextLine();
        }
    }

    public virtual int FeedLines(JSONArray lines){
        Debug.Log("displaying text");
        Debug.Log(lines);
        sentenceIndex = 0;
        description = lines;
        headerName.text = description[sentenceIndex][(int)Line.HEADER_NAME];
        descriptionText.text = description[sentenceIndex][(int)Line.SENTENCE];
        

        return 0;
    }

    protected virtual void DetectNextLine(){
        if (Input.anyKeyDown){
            sentenceIndex++;
            if (sentenceIndex < description.Count) {
                headerName.text = description[sentenceIndex][(int)Line.HEADER_NAME];
                descriptionText.text = description[sentenceIndex][(int)Line.SENTENCE];
            }
            else {
                gameObject.SetActive(false);
            }

        }
    }
}
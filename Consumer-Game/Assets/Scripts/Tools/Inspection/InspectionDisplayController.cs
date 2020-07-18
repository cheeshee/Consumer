using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class InspectionDisplayController : DialogueDisplayController
{
    public override int FeedLines(JSONArray lines){
        Debug.Log("displaying text");
        Debug.Log(lines);
        sentenceIndex = 0;
        description = lines;
        headerName.text = description[sentenceIndex][(int)Line.HEADER_NAME];
        descriptionText.text = description[sentenceIndex][(int)Line.SENTENCE];
        
        // may treat things differently
        // same for now
        return 0;
    }

    // TODO
    // change this as needed, possibly go back and forth between sentences
    protected override void DetectNextLine(){
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class DialogueDisplayController : MonoBehaviour
{
    private GameObject speakerBox;
    private Text speakerName;
    private GameObject textBox;
    private Text dialogueText;
    private JSONArray conversation;

    private enum Line{SPEAKER_NAME, SENTENCE}
    private int sentenceIndex;         // index for sentence in conversation

    
    // Start is called before the first frame update
    void Start()
    {
        speakerBox = transform.GetChild(0).gameObject;
        speakerName = speakerBox.GetComponentInChildren<Text>();
        textBox = transform.GetChild(1).gameObject;
        dialogueText = textBox.GetComponentInChildren<Text>();
        sentenceIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (conversation == null){
            gameObject.SetActive(false);
        } else {
            DetectNextLine();
        }
    }

    public int FeedLines(JSONArray lines){
        Debug.Log("displaying text");
        Debug.Log(lines);
        sentenceIndex = 0;
        conversation = lines;
        speakerName.text = conversation[sentenceIndex][(int)Line.SPEAKER_NAME];
        dialogueText.text = conversation[sentenceIndex][(int)Line.SENTENCE];
        

        return 0;
    }

    private void DetectNextLine(){
        if (Input.anyKeyDown){
            sentenceIndex++;
            if (sentenceIndex < conversation.Count) {
                speakerName.text = conversation[sentenceIndex][(int)Line.SPEAKER_NAME];
                dialogueText.text = conversation[sentenceIndex][(int)Line.SENTENCE];
            }
            else {
                gameObject.SetActive(false);
            }

        }
    }
}
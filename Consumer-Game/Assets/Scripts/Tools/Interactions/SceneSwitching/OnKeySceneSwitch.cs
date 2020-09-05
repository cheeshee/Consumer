using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnKeySceneSwitch : InteractionController
{
    [SerializeField]
    string sceneName;
    protected override void EnterInteraction(){
        if (Input.GetButtonDown("Interact")){
            // Setting UI components
            SceneManager.LoadScene(sceneName); 
        }
    }
}

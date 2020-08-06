using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTriggerSceneSwitch : MonoBehaviour
{
    [SerializeField]
    string sceneName;
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == (int) Layers.Player){
            SceneManager.LoadScene(sceneName);
        }
    }

}

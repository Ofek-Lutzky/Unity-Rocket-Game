using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApp : MonoBehaviour
{
    void Update()
    {
        quitGame();
    }

    void quitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
            Debug.Log("hit escape");
        }

    }
}

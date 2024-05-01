using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene("InitialMenu");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene("TutorialBounce");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene("TutorialWater");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            SceneManager.LoadScene("TutorialStretch");
        }
    }

    public void LevelChanger(String name)
    {
        SceneManager.LoadScene(name);
    }

    public void LeaveLevel() {
        Debug.Log("Leaving Game");
;       SceneManager.LoadScene("LevelSelector");
    }

    public void ResetLevel() {
        Debug.Log("ResetLevel");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

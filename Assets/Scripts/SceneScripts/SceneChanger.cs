using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private string name;

    public void StartGame()
    {
        SceneManager.LoadScene(name);
    }

    public void EndGame()
    {
       Debug.Log("Ha salido correctamente"); 
       Application.Quit();
    }

    /*
    public void SelectGame()
    {

    }
    */
}

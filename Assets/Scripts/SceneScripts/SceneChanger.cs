using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private string startName;
    [SerializeField]
    private string levelName;
    [SerializeField] 
    private GameObject button;

    void Start()
    {
        //button.interactable = false;
        button.SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(startName);
    }

    public void EndGame()
    {
       Debug.Log("Ha salido correctamente"); 
       Application.Quit();
    }

    
    public void SelectLevel()
    {
        SceneManager.LoadScene(levelName);
    }
    
}

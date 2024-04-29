using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get { 
            if (instance == null)
                instance = FindAnyObjectByType<GameManager>();
            return instance;
        }
    }


    [SerializeField] private List<GameObject> checkPoints;
    private int currentIndex = 0;

    public event Action<ColorType> onPlayerDeath;


    [SerializeField] private GameObject pauseMenuPrefab;
    private bool gamePaused = false;

    private void Update()
    {
        ManagePauseGame();    
    }

    private void ManagePauseGame() {
        Debug.Log(pauseMenuPrefab + " " + gamePaused);

        if (gamePaused) {
            pauseMenuPrefab.SetActive(true);
            Time.timeScale = 0f;
        }
        else {
            pauseMenuPrefab.SetActive(false);
            Time.timeScale = 1f;  
        }
    }


    public void MoveToCheckPoint() {
        GameObject player = GameObject.Find("Player");
        player.transform.position = checkPoints[currentIndex].transform.position;
        onPlayerDeath.Invoke(ColorType.Default);
    }

    private void nextCheckPoint(GameObject checkPoint) {
        if (checkPoint != checkPoints[currentIndex]) {
            currentIndex++;
        }
    }

    private void PauseGame() {
        gamePaused = !gamePaused;
    }
    

    private void OnEnable()
    {
        CheckPoint.onCheckPoint += nextCheckPoint;
        PlayerInputs.Instance.onPauseGame += PauseGame;
    }

    private void OnDisable()
    {
        CheckPoint.onCheckPoint -= nextCheckPoint;
        PlayerInputs.Instance.onPauseGame -= PauseGame;
    }
}

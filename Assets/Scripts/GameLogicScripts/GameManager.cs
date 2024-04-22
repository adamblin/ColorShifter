using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> checkPoints;
    private int currentIndex = 0;

    public static event Action<ColorType> onPlayerDeath;
    public static event Action onGameOver;

    public static event Action<int> onLivesChanged;
    [SerializeField] private int playerLives = 3;


    public void MoveToCheckPoint() {
        if (playerLives > 0) {
            GameObject player = GameObject.Find("Player");
            player.transform.position = checkPoints[currentIndex].transform.position;
            onPlayerDeath?.Invoke(ColorType.Default);
        } else {
            onGameOver?.Invoke();
            Debug.Log("Game Over!");
        }
    }

    public void LoseLife() {
        playerLives--;
        onLivesChanged?.Invoke(playerLives);

        if (playerLives <= 0) {
                Debug.Log("No lives left. Game over.");
                onGameOver?.Invoke();
            } else {
                MoveToCheckPoint();
            }
    }

    private void nextCheckPoint(GameObject checkPoint) {
        if (checkPoint != checkPoints[currentIndex]) {
            currentIndex++;
        }
    }

    private void OnEnable()
    {
        CheckPoint.onCheckPoint += nextCheckPoint;
    }

    private void OnDisable()
    {
        CheckPoint.onCheckPoint -= nextCheckPoint;
    }
}

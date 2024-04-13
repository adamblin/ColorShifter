using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> checkPoints;
    private int currentIndex = 0;

    public static event Action<ColorType> onPlayerDeath;
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

    private void OnEnable()
    {
        CheckPoint.onCheckPoint += nextCheckPoint;
    }

    private void OnDisable()
    {
        CheckPoint.onCheckPoint -= nextCheckPoint;
    }
}

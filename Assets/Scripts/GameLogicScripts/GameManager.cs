using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> checkPoints;
    private int currentIndex = 0;

    public void MoveToCheckPoint() {
        GameObject player = GameObject.Find("Player");
        player.transform.position = checkPoints[currentIndex].transform.position;
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

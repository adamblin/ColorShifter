using UnityEngine;
using UnityEngine.UI;

public class LivesDisplay : MonoBehaviour
{
    public int maxLives = 3;  
    public GameObject heartPrefab;
    private GameObject[] hearts;

    private void Start() {
        hearts = new GameObject[maxLives];
        for (int i = 0; i < maxLives; i++) {
            hearts[i] = Instantiate(heartPrefab, transform);
            hearts[i].transform.localPosition = new Vector3(i * 10, 0, 0); 
        }
    }

    private void OnEnable() {
        GameManager.onLivesChanged += UpdateLives;
    }

    private void OnDisable() {
        GameManager.onLivesChanged -= UpdateLives;
    }

    public void UpdateLives(int currentLives) {
        Debug.Log("Updating lives display to " + currentLives + " lives.");
        for (int i = 0; i < maxLives; i++) {
            hearts[i].SetActive(i < currentLives); 
        }
    }
}


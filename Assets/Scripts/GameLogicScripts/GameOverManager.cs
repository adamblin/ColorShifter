using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    void Start() {
        GameManager.onGameOver += HandleGameOver;
    }

    void OnDestroy() {
        GameManager.onGameOver -= HandleGameOver;
    }

    private void HandleGameOver() {
        Debug.Log("Handling game over...");
        
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu() {
        SceneManager.LoadScene("InitialMenu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

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

    [SerializeField] private GameObject pauseUIPrefab;
    private GameObject pauseUIInstance;
    private bool gamePaused = false;

    [SerializeField] private GameObject sceneTransitionPrefab;
    private Animator animator;

    [SerializeField] private float deathTime;

    [SerializeField]
    private ParticleSystem deathPartciles;

    private void Start()
    {
        pauseUIInstance = Instantiate(pauseUIPrefab);

        GameObject prefabInstance = Instantiate(sceneTransitionPrefab);
        animator = prefabInstance.GetComponent<Animator>();
    }

    private void Update()
    {
        ManagePauseGame();    
    }
    private void ManagePauseGame() {
        Debug.Log(pauseUIInstance + " " + gamePaused);

        if (gamePaused)
        {
            pauseUIInstance.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseUIInstance.SetActive(false);
            Time.timeScale = 1f;
        }
    }


    public void MoveToCheckPoint()
    {
        StartCoroutine(WaitForRevive());
    }

    private IEnumerator WaitForRevive()
    {
      
        deathPartciles.Play();
        yield return new WaitForSeconds(deathPartciles.main.duration);
        Debug.Log("Tendrian que salir las particulas");
        deathPartciles.Stop();
        animator.SetTrigger("End");
        yield return new WaitForSeconds(deathTime);
        CharacterMovement.Instance.SetPlayerPosition(checkPoints[currentIndex].transform.position);
        onPlayerDeath.Invoke(ColorType.Default);
        animator.SetTrigger("Start");
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

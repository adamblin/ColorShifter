using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelDoor : MonoBehaviour
{
    [Header("DOOR LOGIC")]
    [SerializeField] private int doorHits;
    [SerializeField] private GameObject doorGameObject;
    [SerializeField] private Sprite openDoorSprite;
    private bool canGoNextLevel = false;

    [Header("NEXT LEVEL")]
    [SerializeField] private string nextLevelName;
    [SerializeField] private String firstLevel;
    public void DoorCollided()
    {
        doorHits--;

        if (doorHits <= 0){
            doorGameObject.GetComponent<SpriteRenderer>().sprite = openDoorSprite;
            canGoNextLevel = true;
            CheckPlayerOnTop();
        }
    }

    private void CheckPlayerOnTop() {
        Collider2D[] collider = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f);
        for (int i = 0; i < collider.Length; i++)
            if (collider[i].gameObject.CompareTag("Player"))
                SceneManager.LoadScene(nextLevelName);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canGoNextLevel) {
            SceneManager.LoadScene(nextLevelName);
        }

        //Logica para mostrar el nivel 1
        //if (SceneManager.GetActiveScene().name == firstLevel)
        //{
        //    Debug.Log("Es el primer nivel");
        //}
        //else
        //{
        //    Debug.Log("No es el primer nivel");
        //}
    }
}

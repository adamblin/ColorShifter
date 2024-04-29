using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelDoor : MonoBehaviour
{
    [SerializeField] private int doorHits;
    [SerializeField] private string nextLevelName;
    [SerializeField] private String firstLevel;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DoorCollided()
    {
        doorHits--;

        if (doorHits <= 0) { 
            GetComponent<BoxCollider2D>().isTrigger = true;
            spriteRenderer.color = Color.green;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        //Logica para mostrar el nivel 1
        if (SceneManager.GetActiveScene().name == firstLevel)
        {
            Debug.Log("Es el primer nivel");
        }
        else
        {
            Debug.Log("No es el primer nivel");
        }
        SceneManager.LoadScene(nextLevelName);
    }
}

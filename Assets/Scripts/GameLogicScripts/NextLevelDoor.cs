using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelDoor : MonoBehaviour
{
    [SerializeField] private int doorHits;
    [SerializeField] private string nextLevelName;

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
        SceneManager.LoadScene(nextLevelName);
    }
}

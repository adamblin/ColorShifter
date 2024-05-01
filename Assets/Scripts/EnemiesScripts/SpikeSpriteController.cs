using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpriteController : MonoBehaviour
{
    [SerializeField] int spriteSelected = 1;
    [SerializeField] Sprite[] possibleSprites;
    private void Start()
    {
        Debug.Log(possibleSprites.Length);
        if(spriteSelected > possibleSprites.Length-1 || spriteSelected < 0)
            GetComponent<SpriteRenderer>().sprite = possibleSprites[0];
        else
            GetComponent<SpriteRenderer>().sprite = possibleSprites[spriteSelected];
    }
}

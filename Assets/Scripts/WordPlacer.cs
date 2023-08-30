using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordPlacer : MonoBehaviour
{
    public string currentWord;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Word Slot"))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.GetComponent<WordSlot>().SetWord(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSlot : MonoBehaviour
{
    [SerializeField] private GameObject currentVerse;
    private bool correctVerseAdded = false;

    [Header("Progress Tracking")]
    public string correctVerse;

    public void SetWord(GameObject newWord)
    {
        currentVerse = newWord;

        GetComponent<BoxCollider2D>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);

        newWord.transform.position = transform.position;
        newWord.GetComponent<Draggable>().canBeDragged = false;

        if(correctVerse == currentVerse.GetComponent<WordPlacer>().currentWord)
        {
            correctVerseAdded = true;
        }

        FindObjectOfType<MinigameController_PoemArranger>().UpdateResults(correctVerseAdded);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MinigameController_PoemArranger : MonoBehaviour
{
    [Header("Poem Parts")]
    [SerializeField] private List<string> verses;

    [Header("Needed References")]
    [SerializeField] private GameObject wordBoxPrefab;
    [SerializeField] private GameObject wordBoxParent;
    [SerializeField] private GameObject wordSlotPrefab;
    [SerializeField] private GameObject wordSlotParent;

    [Header("End Screen")]
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TextMeshProUGUI resultText;

    [Header("Debugging")]
    [SerializeField] private int verseAmount;
    [SerializeField] private List<string> verseCopies;
    [SerializeField] private int correctVerses;
    [SerializeField] private int versesPlaced;

    private void Start()
    {
        verseAmount = verses.Count;

        foreach(string verse in verses)
        {
            verseCopies.Add(verse);
        }

        for(int i = 0; i < verseAmount; i++)
        {
            GameObject newVerse = Instantiate(wordBoxPrefab, wordBoxParent.transform);

            int verseIndex = Random.Range(0, verseCopies.Count);

            newVerse.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = verseCopies[verseIndex];
            newVerse.GetComponent<WordPlacer>().currentWord = verseCopies[verseIndex];

            verseCopies.RemoveAt(verseIndex);

            GameObject newSlot = Instantiate(wordSlotPrefab, wordSlotParent.transform);

            newSlot.GetComponent<WordSlot>().correctVerse = verses[i];
        }
    }

    public void UpdateResults(bool success)
    {
        if (success)
        {
            correctVerses++;
        }

        versesPlaced++;

        if(versesPlaced >= verseAmount)
        {
            FindObjectOfType<Timer>().StopTimer();
            ShowEndScreen();
        }
    }

    public void ShowEndScreen()
    {
        if(versesPlaced >= verseAmount)
        {
            resultText.text = "You got " + correctVerses + " verses correctly.";
        }

        else
        {
            resultText.text = "You run out of time.";
        }

        endScreen.SetActive(true);
    }

    public void RestartMinigame()
    {
        SceneManager.LoadScene("Minigame_Word");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("WorldView");
    }
}

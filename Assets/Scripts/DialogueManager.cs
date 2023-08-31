using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue Canvas")]
    [SerializeField] private GameObject dialogueCanvas;

    [Header("Dialogue UI")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject continueButton;

    [Header("Quest Choices")]
    [SerializeField] private GameObject[] choiceButtons;
    [SerializeField] private TextMeshProUGUI[] choicesText;

    // private variables
    private Story currentStory;

    [Header("Public Values for References")]
    public bool dialogueIsPlaying;

    private static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is more than one Dialogue Manager in the scene!");
        }

        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueCanvas.SetActive(false);
        dialogueIsPlaying = false;

        choicesText = new TextMeshProUGUI[choiceButtons.Length];
        int index = 0;

        foreach (GameObject choice in choiceButtons)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            choice.SetActive(false);
            index++;
        }
    }

    public void StartDialogue(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialogueCanvas.SetActive(true);

        ContinueDialogue();
    }

    public void ContinueDialogue()
    {
        Debug.Log("Pressed Continue button.");

        if (currentStory.canContinue)
        {
            Debug.Log("Dialogue about to continue.");

            dialogueText.text = currentStory.Continue();

            //HandleTags(currentStory.currentTags);

            DisplayChoices();
        }

        else
        {
            EndDialogue();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > 0)
        {
            continueButton.gameObject.SetActive(false);
        }

        else
        {
            continueButton.gameObject.SetActive(true);
        }

        if (currentChoices.Count > choiceButtons.Length)
        {
            Debug.LogWarning("There are more choices written than the UI can hold!");
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choiceButtons[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].gameObject.SetActive(false);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        Debug.Log("Made a choice.");
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueDialogue();
    }

    /*private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(":");

            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case selectedQuest:

                    Quest newQuest = new Quest();

                    switch (tagValue)
                    {
                        case "broccoli":
                            newQuest.target = KillQuestTarget.broccoli;
                            newQuest.neededAmountOfKills = 7;
                            break;

                        case "cabbage":
                            newQuest.target = KillQuestTarget.cabbage;
                            newQuest.neededAmountOfKills = 2;
                            break;

                        case "carrot":
                            newQuest.target = KillQuestTarget.carrot;
                            newQuest.neededAmountOfKills = 6;
                            break;
                    }

                    QuestManager.GetInstance().SetNewQuest(newQuest);

                    break;

                case potatoName:

                    switch (tagValue)
                    {
                        case "talking":
                            potatoNameText.text = "The Talking Potato";
                            break;

                        case "racist":
                            //potatoNameText.text = "The " + "Talking" + "Racist Potato";
                            potatoNameText.text = "The " + "<s>Talking</s> " + "<b>Racist </b>" + "Potato";
                            break;
                    }
                    break;
            }
        }
    }*/

    private void EndDialogue()
    {
        Debug.Log("Dialogue ending.");
        dialogueIsPlaying = false;
        dialogueText.text = "";

        dialogueCanvas.SetActive(false);

        SceneManager.LoadScene("Minigame_Word");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtPreCreationPanel : MonoBehaviour
{
    public GameObject selectedStyle;
    public GameObject selectedArt;

    public void SetSelectedInfo(string type, GameObject newButton)
    {
        switch (type)
        {
            case "style":
                if(selectedStyle != null)
                {
                    selectedStyle.transform.GetChild(0).gameObject.SetActive(false);
                }

                selectedStyle = newButton;

                break;

            case "art":
                if (selectedArt != null)
                {
                    selectedArt.transform.GetChild(0).gameObject.SetActive(false);
                }

                selectedArt = newButton;

                break;

            default:
                Debug.LogError("No art precreation button type detected.");
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfficeButtons : MonoBehaviour
{
    [SerializeField] private GameObject artView;
    [SerializeField] private GameObject timeline;

    public void ShowArtView()
    {
        artView.SetActive(true);
        timeline.SetActive(false);
    }

    public void ShowTimeline()
    {
        artView.SetActive(false);
        timeline.SetActive(true);
    }

    public void ShowArtPreCreationPanel()
    {
        artView.transform.GetChild(3).gameObject.SetActive(true);
    }

    public void MoveToArtCreation()
    {
        SceneManager.LoadScene("ArtPuzzle");
    }
}

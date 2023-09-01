using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArtPreCreationButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string type;

    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<ArtPreCreationPanel>().SetSelectedInfo(type, gameObject);
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}

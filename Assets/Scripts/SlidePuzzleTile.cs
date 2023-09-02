using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePuzzleTile : MonoBehaviour
{
    public Vector3 correctPosition;

    private ArtSlidePuzzleManager gameManager;
    private bool isMoving;

    private void Start()
    {
        gameManager = FindObjectOfType<ArtSlidePuzzleManager>();
    }

    public void MoveToEmptySpace(Vector3 targetPosition)
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(Move(targetPosition));
        }
    }
    
    private IEnumerator Move(Vector3 targetPosition)
    {
        if(transform.localPosition == correctPosition)
        {
            gameManager.UpdateProgress(-1);
            Debug.Log("wrong place");
        }

        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 0.005f);

            yield return null;
        }

        isMoving = false;

        if(transform.localPosition == correctPosition)
        {
            gameManager.UpdateProgress(1);
            Debug.Log("correct place");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private float startTime;

    [Header("Needed References")]
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Debugging")]
    [SerializeField] private Coroutine currentTimer;

    private void Start()
    {
        timerText.text = "" + startTime;

        currentTimer = StartCoroutine(TimerCounter());
    }

    private IEnumerator TimerCounter()
    {
        while(startTime > 0)
        {
            yield return new WaitForSeconds(1f);

            startTime--;
            timerText.text = "" + startTime;
        }

        FindObjectOfType<MinigameController_PoemArranger>().ShowEndScreen();
    }

    public void StopTimer()
    {
        StopCoroutine(currentTimer);
    }
}

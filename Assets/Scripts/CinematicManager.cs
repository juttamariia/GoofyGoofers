using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> cinematicImages;
    [SerializeField] private int currentIndex = 0;

    [Header("Camera Movement")]
    [SerializeField] private List<CinematicCameraChanges> cameraChanges;

    [Header("Fade")]
    [SerializeField] private bool fadingIn = false;
    [SerializeField] private bool fadingOut = false;
    [SerializeField] private float fadePause = 1f;
    [SerializeField] private Color fadeState = new Color(0, 0, 0, 0);

    [Header("Needed References")]
    [SerializeField] private SpriteRenderer currentImage;
    [SerializeField] private SpriteRenderer fadeScreen;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        currentImage.sprite = cinematicImages[currentIndex];

        SetCameraStartValues();
    }

    private void Update()
    {
        if (!fadingIn && !fadingOut)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (currentIndex < cinematicImages.Count)
                {
                    fadingIn = true;
                    StartCoroutine(ChangeImageFade());
                }
            }

            if (cameraChanges[currentIndex].startingPosition != cameraChanges[currentIndex].endPosition)
            {
                Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, cameraChanges[currentIndex].endPosition, ref velocity, cameraChanges[currentIndex].moveSpeed * Time.deltaTime);
            }

            if (Camera.main.orthographicSize < cameraChanges[currentIndex].endSize)
            {
                Camera.main.orthographicSize += cameraChanges[currentIndex].sizeChange * Time.deltaTime;
            }
        }
        
    }

    private void SetCameraStartValues()
    {
        Camera.main.orthographicSize = cameraChanges[currentIndex].startSize;
        Camera.main.transform.position = cameraChanges[currentIndex].startingPosition;
    }

    private IEnumerator ChangeImageFade()
    {
        fadeState = fadeScreen.color;

        while (fadingIn)
        {
            fadeState = new Color(0, 0, 0, fadeState.a + 0.01f);
            fadeScreen.color = fadeState;

            if(fadeScreen.color.a >= 1)
            {
                fadingIn = false;
                yield return new WaitForSeconds(fadePause);
            }
            yield return new WaitForSeconds(0.01f);
        }

        currentIndex++;
        currentImage.sprite = cinematicImages[currentIndex];

        SetCameraStartValues();

        fadingOut = true;

        while (fadingOut)
        {
            fadeState = new Color(0, 0, 0, fadeState.a - 0.01f);
            fadeScreen.color = fadeState;

            if (fadeScreen.color.a <= 0)
            {
                fadingOut = false;
                yield return null;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}

[System.Serializable]
public class CinematicCameraChanges
{
    public Vector3 startingPosition;
    public Vector3 endPosition;
    public float startSize;
    public float endSize;
    public float moveSpeed = 2f;
    public float sizeChange = 2f;
}

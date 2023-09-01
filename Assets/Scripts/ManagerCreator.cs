using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerCreator : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.LoadScene("MainMenu");
    }
}

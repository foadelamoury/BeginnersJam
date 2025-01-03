using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        AudioManager.Instance.StopMusic();
        SceneManager.LoadScene(nextSceneIndex);
    }
}

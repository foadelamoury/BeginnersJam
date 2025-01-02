using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        AudioManager.Instance.StopMusic();
        SceneManager.LoadSceneAsync("City");
    }
}

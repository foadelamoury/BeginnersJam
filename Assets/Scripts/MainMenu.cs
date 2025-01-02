using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        AudioManager.Instance.StopMusic(); // Stop the background music
        Debug.Log("Background music stopped!");
        SceneManager.LoadSceneAsync("SampleScene");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
<<<<<<< Updated upstream
        SceneManager.LoadSceneAsync("Level1");
=======
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
>>>>>>> Stashed changes
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
<<<<<<< Updated upstream
        SceneManager.LoadSceneAsync("Bunker");
=======
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
>>>>>>> Stashed changes
    }
}

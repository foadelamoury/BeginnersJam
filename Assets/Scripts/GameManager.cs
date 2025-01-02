using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioClip backgroundMusic; // Assign in the Inspector

    void Start()
    {
        // Play the background music on loop
        AudioManager.Instance.PlayMusic(backgroundMusic);
    }
}

using UnityEngine;

public class FenceSystemManager : MonoBehaviour
{
    public float exitTimerDuration = 5f; // Time allowed for the player to enter the next trigger
    private float timer = 0f;
    private bool isTimerActive = false;
    private PlayerMovment player;
    [SerializeField] public bool lastBox = false;

    public void PlayerEnteredFence(FenceTriggerManager fence)
    {
        Debug.Log("Player entered the fence at: " + fence.gameObject.name);

        // Reset the timer and stop checking for failure
        isTimerActive = false;
        timer = 0f;

        // Check if the current fence is the last box
        if (fence.lastBox)
        {
            Debug.Log("Last box reached. Timer reset and timeout disabled.");
        }
    }

    public void PlayerExitedFence(FenceTriggerManager fence)
    {
        Debug.Log("Player exited the fence: " + fence.gameObject.name);

        if (!fence.lastBox) // Only start the timer if the current fence is not the last box
        {
            isTimerActive = true;
            timer = exitTimerDuration;
        }
        else
        {
            Debug.Log("Player exited the last box. Timer will not be activated.");
        }
    }

    private void Update()
    {
        if (isTimerActive)
        {
            timer -= Time.deltaTime;
            Debug.Log("Timer running: " + timer);

            if (timer <= 0f)
            {
                Debug.LogWarning("Player failed to reach the next fence in time!");
                isTimerActive = false;

                if (player != null)
                {
                    Debug.Log("Calling Die() on the player.");
                    player.Die();
                }
                else
                {
                    Debug.LogError("Player reference is null. Die() cannot be called!");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerMovment>();
            if (player != null)
            {
                Debug.Log("PlayerMovment script found and assigned successfully.");
            }
            else
            {
                Debug.LogError("PlayerMovment script not found on the Player GameObject!");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the system trigger.");
        }
    }
}

using UnityEngine;

public class FenceSystemManager : MonoBehaviour
{
    public float exitTimerDuration = 5f;
    public float timer = 0f;
    public bool isTimerActive = false;
    public PlayerMovment player;
    //[SerializeField] public bool lastBox = false;


    public void PlayerEnteredFence(FenceTriggerManager fence)
    {

        Debug.Log("Player entered the fence at: " + fence.gameObject.name);

        isTimerActive = false;
        timer = 0f;

        if (fence.lastBox)
        {
            Debug.Log("Last box reached. Timer reset and timeout disabled.");
        }
    }

    public void PlayerExitedFence(FenceTriggerManager fence)
    {

        Debug.Log("Player exited the fence: " + fence.gameObject.name);

        if (!fence.lastBox)
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

}
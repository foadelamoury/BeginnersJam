using UnityEngine;

public class FenceTriggerManager : MonoBehaviour
{
    private FenceSystemManager fenceSystemManager;

    // Indicates if this fence is marked as the last box
    [SerializeField] public bool lastBox = false;

    private void Start()
    {
        fenceSystemManager = FindObjectOfType<FenceSystemManager>();
        if (fenceSystemManager == null)
        {
            Debug.LogError("FenceSystemManager not found in the scene!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the fence trigger: " + gameObject.name);

            // Notify the FenceSystemManager about the player's entry
            fenceSystemManager.PlayerEnteredFence(this);

            if (lastBox)
            {
                Debug.Log("Player entered the last box: " + gameObject.name);
            }
            else
            {
                Debug.Log("Player entered a normal box: " + gameObject.name);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the fence trigger: " + gameObject.name);

            // Notify the FenceSystemManager about the player's exit
            fenceSystemManager.PlayerExitedFence(this);

            if (lastBox)
            {
                Debug.Log("Player exited the last box: " + gameObject.name);
            }
            else
            {
                Debug.Log("Player exited a normal box: " + gameObject.name);
            }
        }
    }
}

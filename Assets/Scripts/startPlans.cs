using UnityEngine;

public class StartPlanes : MonoBehaviour
{
    [SerializeField] private GameObject planeSpawner;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            if (planeSpawner != null) 
            {
                planeSpawner.SetActive(true);
                 
            }
            else
            {
                Debug.LogWarning("PlaneSpawner is not assigned in the Inspector!");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        planeSpawner.SetActive(false);
    }
}

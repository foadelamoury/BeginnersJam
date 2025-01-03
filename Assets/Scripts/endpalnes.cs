using UnityEngine;

public class endpalnes : MonoBehaviour
{
    [SerializeField] private GameObject planeSpawner;
    [SerializeField] private bool isActive = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (planeSpawner != null)
            {
                planeSpawner.SetActive(isActive);
                isActive = !isActive;
            }
            else
            {
                Debug.LogWarning("PlaneSpawner is not assigned in the Inspector!");
            }
        }
    }
}


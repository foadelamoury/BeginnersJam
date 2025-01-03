using UnityEngine;

public class ObjectToggler : MonoBehaviour
{
    [SerializeField] private GameObject box;
    private bool isInRange = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        isInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isInRange = false;
    }

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            gameObject.SetActive(false);
            box.SetActive(true);
        }
    }
}
using UnityEngine;

public class ObjectToggler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameObject.SetActive(false);
            
        }
    }
}
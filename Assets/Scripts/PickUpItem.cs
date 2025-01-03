using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private bool isPlayerInRange = false;

    public Canvas pickUpCanvas;
    private bool picked = false;
    [SerializeField] public AudioClip Sound;
    void Update()
    {
        // Check if the player presses 'E' while in range
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {

            PickUp();
            pickUpCanvas.gameObject.SetActive(false);
            picked = true;
            AudioManager.Instance.PlaySFX(Sound);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detect if the player enters the trigger collider
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (picked == false)
            {
            pickUpCanvas.gameObject.SetActive(true);

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Detect if the player leaves the trigger collider
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            pickUpCanvas.gameObject.SetActive(false);

        }
    }

    private void PickUp()
    {
        // Logic for when the object is picked up
        Debug.Log($"{gameObject.name} picked up!");
    }
}
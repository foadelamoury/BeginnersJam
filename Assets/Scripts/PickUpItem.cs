using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private bool isPlayerInRange = false;

    public Canvas pickUpCanvas;
    private BoxCollider2D deadSoldierCol;
    public AudioClip tagTaken;

    private void Start()
    {
        deadSoldierCol = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Check if the player presses 'E' while in range
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {

            PickUp();
            pickUpCanvas.gameObject.SetActive(false);


        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detect if the player enters the trigger collider
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            pickUpCanvas.gameObject.SetActive(true);
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

        // Example: Deactivate the object
        //gameObject.SetActive(false);
        deadSoldierCol.enabled = false;
        AudioManager.Instance.PlaySFX(tagTaken);
      

        // Alternatively, you could notify the player script or add to inventory
    }
}
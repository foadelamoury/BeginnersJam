using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PickUpItem : MonoBehaviour
{
    private bool isPlayerInRange = false;

    public AudioClip tagTaken;
    public TextMeshProUGUI scoreText;

    public Canvas pickUpCanvas;
    private bool picked = false;

    void Start()
    {
        if (PlayerPrefs.GetInt("PlayerScore") == 0)
        {
            scoreText.text = "Dog tags collected : " + PlayerPrefs.GetInt("PlayerScore").ToString();

        }
    }

    void Update()
    {
        // Check if the player presses 'E' while in range
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {

            PickUp();
            pickUpCanvas.gameObject.SetActive(false);
            picked = true;
            
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

        // Example: Deactivate the object
        //gameObject.SetActive(false);

        int currentScore = PlayerPrefs.GetInt("PlayerScore", 0) + 1;
        AudioManager.Instance.PlaySFX(tagTaken);

        PlayerPrefs.SetInt("PlayerScore", currentScore);
        PlayerPrefs.Save();
        scoreText.text="Dog tags collected : " + PlayerPrefs.GetInt("PlayerScore").ToString();
        // Alternatively, you could notify the player script or add to inventory
    }
}
using System.Collections;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public float speed = 5f;
    public float endX = 100f;
    public GameObject rocketPrefab;  // Assign the rocket prefab in the Inspector
    public float fireRate = 0.5f;      // Time between rocket fires (shortened for testing)
    public int rocketsToThrow = 3;   // Number of rockets to throw
    public Vector3 rocketOffset = new Vector3(2f, 0f, 0f); // Offset for where rockets spawn
    public AudioClip startSound;
    private int rocketsThrown = 0;

    void Start()
    {
        // Start the rocket throwing coroutine
        StartCoroutine(ThrowRockets());
        AudioManager.Instance.PlaySFX(startSound);
    }

    void Update()
    {
        // Move the plane
        transform.position += Vector3.right * speed * Time.deltaTime;

        // Destroy the plane if it reaches the end
        if (transform.position.x >= endX)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ThrowRockets()
    {
        while (rocketsThrown < rocketsToThrow)
        {
            Debug.Log("Rocket thrown at: " + Time.time); // Debug log for when the rocket is thrown
            yield return new WaitForSeconds(fireRate);  // Delay the next rocket launch

            // Spawn a rocket slightly in front of the plane
            Instantiate(rocketPrefab, transform.position + rocketOffset, Quaternion.identity);

            // Increment the number of rockets thrown
            rocketsThrown++;

            // Wait before throwing the next rocket (based on the fireRate)
            Debug.Log("Waiting for next rocket: " + fireRate + " seconds");
        }
    }
}

using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class PlaneSpawner : MonoBehaviour
{
    public GameObject planePrefab; // Assign your plane prefab
    public Vector3 spawnPosition = new Vector3(-8, 3, 0); // Adjust as needed
    public float respawnDelay = 40f; // Respawn time in seconds
    public float endX = 100f; // Plane's X end boundary
    public float planeSpeed = 5f; // Plane's speed

    
    void OnEnable()
    {
        StartCoroutine(SpawnPlane());
    }

    IEnumerator SpawnPlane()
    {
        while (true)
        {
            // Instantiate the plane
            GameObject plane = Instantiate(planePrefab, spawnPosition, Quaternion.identity);
            Plane planeScript = plane.GetComponent<Plane>();
            planeScript.speed = planeSpeed;
            planeScript.endX = endX;

            // Wait for the respawn delay
            yield return new WaitForSeconds(respawnDelay);
        }
    }
}

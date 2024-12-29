using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    public Transform[] layers; // Array of layers
    public float[] parallaxScales; // Proportion of the camera's movement for each layer
    public float smoothing = 1f; // How smooth the parallax is (set > 0)
    public Camera mainCamera; // Reference to the main camera

    private Vector3 previousCameraPosition;
    private float[] layerWidths; // Store widths of each layer

    void Start()
    {
        // Initialize camera position and layer widths
        previousCameraPosition = mainCamera.transform.position;

        layerWidths = new float[layers.Length];
        for (int i = 0; i < layers.Length; i++)
        {
            SpriteRenderer spriteRenderer = layers[i].GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                layerWidths[i] = spriteRenderer.bounds.size.x;
            }
            else
            {
                Debug.LogWarning($"Layer {layers[i].name} does not have a SpriteRenderer!");
            }
        }
    }

    void Update()
    {
        Vector3 cameraMovement = mainCamera.transform.position - previousCameraPosition;

        // Apply parallax effect and check for looping
        for (int i = 0; i < layers.Length; i++)
        {
            if (i >= parallaxScales.Length) break;

            // Parallax movement
            float parallax = cameraMovement.x * parallaxScales[i];
            layers[i].position += new Vector3(parallax, 0, 0);

            // Check if the layer has left the screen on the left
            float layerRightEdge = layers[i].position.x + layerWidths[i] / 2f;
            float cameraLeftEdge = mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect;

            if (layerRightEdge < cameraLeftEdge)
            {
                // Reposition the layer to the right side of the camera
                float layerLeftEdge = layers[i].position.x - layerWidths[i] / 2f;
                float cameraRightEdge = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;

                layers[i].position = new Vector3(cameraRightEdge + (layerWidths[i] / 2f), layers[i].position.y, layers[i].position.z);
            }
        }

        // Update previousCameraPosition
        previousCameraPosition = mainCamera.transform.position;
    }
}
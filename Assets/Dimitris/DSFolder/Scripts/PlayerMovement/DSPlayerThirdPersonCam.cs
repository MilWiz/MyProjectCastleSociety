using UnityEngine;

public class DSPlayerThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform player; // Reference to the player transform
    public Transform playerObj; // Reference to the player object transform
    public Transform orientation; // Reference to the orientation transform
    public Rigidbody playerRB; // Reference to the player Rigidbody component

    [Header("Camera Settings")]
    public float rotationSpeed = 5f; // Speed of camera rotation

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x , player.position.y , transform.position.z); // Calculate the direction from camera to player
        orientation.forward = viewDir.normalized; // Set the orientation forward direction to the view direction

        float horizontalInput = Input.GetAxis("Horizontal"); // Get horizontal mouse input
        float verticalInput = Input.GetAxis("Vertical"); // Get vertical mouse input

        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput; // Calculate input direction based on mouse input

        if (inputDir != Vector3.zero) // If there is input direction
        {
            playerObj.forward = inputDir.normalized; Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed); // Smoothly rotate the player object towards the input direction
        }
    }
}

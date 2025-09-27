using UnityEngine;

public class DSPlayerController : MonoBehaviour
{
    [Header("Track")]
    public Transform playerTransform; // Reference to the player's transform

    [Header("Keybinds")]
    public KeyCode forwardKey = KeyCode.W;
    public KeyCode backwardKey = KeyCode.S ;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode jumpKey = KeyCode.Space;








    Rigidbody rbody;
    Vector3 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }


    
    void FixedUpdate()
    {
       
    }
}

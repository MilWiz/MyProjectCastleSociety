using UnityEngine;

public class DSPlayerAnimation : MonoBehaviour
{

    public Animator playerObjAnimator; // Reference to the Animator component for player animations
    public DSPlayerMovement playerMovement; // Reference to the DSPlayerMovement script for player movement logic

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = playerObjAnimator.GetCurrentAnimatorStateInfo(0);

        Vector3 vel = playerMovement.currentVelocity;
        // Ταχύτητα στην κατεύθυνση "μπροστά" του orientation
        float forwardSpeed = Vector3.Dot(vel, playerMovement.orientation.forward);

        // Ταχύτητα στην κατεύθυνση "δεξιά" του orientation
        float rightSpeed = Vector3.Dot(vel, playerMovement.orientation.right);

        playerObjAnimator.SetFloat("VelocityZ", Mathf.Abs(playerMovement.currentSpeed / playerMovement.runSpeed));
        playerObjAnimator.SetFloat("VelocityX", 0);
        playerObjAnimator.SetFloat("Croutching", playerMovement.crouchingPerc);

        if (playerMovement.currentState == DSPlayerMovement.PlayerState.Croutching)
        {
            playerObjAnimator.SetFloat("VelocityY", 0);
        }
        else if (playerMovement.currentState == DSPlayerMovement.PlayerState.Moving)
        {
            playerObjAnimator.SetFloat("VelocityY", 0);
        }
        else if (playerMovement.currentState == DSPlayerMovement.PlayerState.Jumping)
        {
            playerObjAnimator.SetFloat("VelocityY", Mathf.Clamp01(Mathf.Pow(playerMovement.currentJumpPerc, 1.8f)));
        }


        if (playerMovement.onPause)
        {
            playerObjAnimator.speed = 0f; // Stop the animation when paused
        }
        else
        {
            playerObjAnimator.speed = 1f; // Resume normal animation speed when not paused
        }
    }
}

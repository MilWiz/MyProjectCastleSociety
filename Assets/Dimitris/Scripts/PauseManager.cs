using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseManagerDS : MonoBehaviour
{

    //public FirstPersonController PlayerCamera;
    public DSPlayerMovement PlayerCamera;
    public List<Manager> managers; // List to hold all manager instances

    // is responsible 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //PlayerCamera.enabled = false;
        PlayerCamera.onPause = true;
        Time.timeScale = 0f;    // Pauses The Game
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;    // UnPauses The Game
        //PlayerCamera.enabled = true;
        PlayerCamera.onPause = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void HalfTimesSpeed()
    {
        Time.timeScale = 0.5f; // Make everything run at half times the speed.
    }


    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0f; // Pause the game
        }
        else if (Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = 1f; // Resume the game
        }
        */

    }

    
}

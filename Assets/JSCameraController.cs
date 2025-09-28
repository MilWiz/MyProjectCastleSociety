using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
using System.Net;
#endif

public class JSCameraController : MonoBehaviour
{

    public KeyCode MoveFrontKey, MoveBackKey, MoveLeftKey, MoveRightKey;
    public KeyCode JumpKey, CroutchKey, MenuKey, RunKey , AimProtectKey , AttackKey ;
    // aim / protect key is for making a shield with a sword or aim with a gun
    // Attack key is for attacking with sword or shoot with the gun
    public float WalkSpeed, RunSpeed , croutchSpeed;
    public bool EnableCamera;
    public GameObject  MenuObject , CameraObject;






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (EnableCamera == false)
        {
            CameraObject.SetActive(false);
            return;
        }

        CameraObject.SetActive(true);
        Camera cam = CameraObject.GetComponent<Camera>();

        if (Input.GetKeyDown(MoveFrontKey))
        {
            // move front
           
        }

        if (Input.GetKeyDown(MoveBackKey))
        {
            // move back
        }

        if (Input.GetKeyDown(MoveLeftKey))
        {
            // move left
        }

        if (Input.GetKeyDown(MoveRightKey))
        { 
            // move right        
        }

        if (Input.GetKeyDown(JumpKey))
        {
            // jump
        }
        else if (Input.GetKeyDown(CroutchKey))
        {
            // croutch
        }
        else if (Input.GetKeyDown(RunKey))
        {
            // Run
        }

        if (Input.GetKeyDown(AimProtectKey))
        {
            // aim / protect
        }

        if (Input.GetKeyDown(AttackKey))
        {
            // attack
        }


    }


    public void MakeCursorLockedAndInvisible()
    {
        Cursor.visible = false;
        Screen.lockCursor = true;
    }
}

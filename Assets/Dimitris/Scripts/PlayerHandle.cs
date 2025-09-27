using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
using System.Net;
#endif
public class PlayerHandle : MonoBehaviour
{
    #region Player
    public GameObject Player;
    //public UnityEditor.Menu PauseMenu;
    #region Keys
    public KeyCode MoveFrontKey, MoveBackKey, MoveLeftKey, MoveRightKey;
    public KeyCode RunKey;
    public KeyCode PauseMenuKey;
    #endregion
    #region Properties Variables
    public Vector3 InitPosition;
    public Quaternion InitRotation;
    public float RunSpeed , WalkSpeed;
    #endregion
    #endregion

    private Vector3 Up;
    private Vector3 View;
    private Vector3 Right;
    // right hand rule

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player.transform.position = InitPosition;
        Player.transform.rotation = InitRotation;
    }

    // Update is called once per frame
    void Update()
    {
        float Speed = WalkSpeed ;

        if (Input.GetKeyDown(RunKey))
        {
            Speed = RunSpeed;
        }

        if (Input.GetKeyDown(MoveBackKey))
        {
            // move back
            Speed = -Speed;
        }

        if (Input.GetKeyDown(MoveFrontKey))
        {
            // Move Font 
        }

        if (Input.GetKeyDown(MoveLeftKey))
        {
            // Move left
        }

        if (Input.GetKeyDown(MoveRightKey))
        {
            // Move Right
        }

        


    }

    void Caculate()
    {
        
        Matrix4x4 mat = new Matrix4x4();

    }
}

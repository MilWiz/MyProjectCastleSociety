using Unity.VisualScripting;
using UnityEngine;

public class Map2 : MonoBehaviour
{
    public KeyCode OpenMapKey;
    public KeyCode PauseMenuKey;
    public GameObject PauseMenu,Mapp,con;
    public GameObject GameIsRunning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(GameIsRunning==true){
            Cursor.visible = false;
            Screen.lockCursor = true;
            if(Input.GetKeyDown(OpenMapKey)){
                //Debug.Log("M key has been pressed");
                Cursor.visible = true;
                Screen.lockCursor = false;
                Mapp.SetActive(true);
            }

            if(Input.GetKeyDown(PauseMenuKey)){
                //Debug.Log("ESC key has been pressed");
                PauseMenu.SetActive(true);
                Cursor.visible = true;
                Screen.lockCursor = false;
            }
        }
    }

    public void MakeCursorLockedAndInvisible()
    {
        Cursor.visible = false;
        Screen.lockCursor = true;
    }


    public void GoToLocation(GameObject Destination)
    {
        con.transform.position = Destination.transform.position;
    }
}

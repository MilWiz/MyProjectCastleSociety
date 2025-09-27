using Unity.VisualScripting;
using UnityEngine;

public class Map : MonoBehaviour
{
    public KeyCode OpenMapKey;
    public KeyCode PauseMenuKey;
    public KeyCode InteractKey;
    public GameObject PauseMenu, Mapp, optionsmenu, exitmenu, areyousure, creditsmenu, mainmenu, NPCPanel;
    public GameObject GameIsRunning;
    public FirstPersonController a;
    public bool temp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        temp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if( a != null )
        {
            if (GameIsRunning.activeSelf && !temp)
            {
                if (NPCPanel.activeSelf == false)
                {
                    if (Input.GetKeyDown(OpenMapKey))
                    {
                        //Debug.Log("M key has been pressed");
                        temp = true;
                        a.enabled = false;
                        GameIsRunning.SetActive(true);
                        Cursor.visible = true;
                        Screen.lockCursor = false;
                        Mapp.SetActive(true);
                    }
                    else if (Input.GetKeyDown(PauseMenuKey))
                    {
                        //Debug.Log("ESC key has been pressed");
                        temp = true;
                        a.enabled = false;
                        Cursor.visible = true;
                        Screen.lockCursor = false;
                        PauseMenu.SetActive(true);
                    }
                }
            }
            if(PauseMenu.activeSelf == false 
            && Mapp.activeSelf == false 
            && optionsmenu.activeSelf == false 
            && exitmenu.activeSelf == false 
            && areyousure.activeSelf == false 
            && creditsmenu.activeSelf == false 
            && mainmenu.activeSelf == false
            && NPCPanel.activeSelf == false){
                temp = false;
                Cursor.visible = false;
                Screen.lockCursor = true;
            }
        }
            else{
                Debug.Log("Error creating");
            }  
    }

    public void MakeCursorLockedAndInvisible()
    {
        Cursor.visible = false;
        Screen.lockCursor = true;
    }


    public void GoToLocation(GameObject Destination)
    {
        a.transform.position = Destination.transform.position;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class TheMap : MonoBehaviour
{
    public Button Back;
    public Button Location1Button, Location2Button, Location3Button, Location4Button;
    public GameObject Location1, Location2, Location3,  Location4;
    public GameObject Map;
    public FirstPersonController a;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        a.GetComponent<FirstPersonController>();
        Back.onClick.AddListener(OnBackButtonClick);
        Location1Button.onClick.AddListener(OnLocation1ButtonClick);
        Location2Button.onClick.AddListener(OnLocation2ButtonClick);
        Location3Button.onClick.AddListener(OnLocation3ButtonClick);
        Location4Button.onClick.AddListener(OnLocation4ButtonClick);
    }

    void OnBackButtonClick(){
        Map.SetActive(false);
        Cursor.visible = false;
        Screen.lockCursor = true;
        if(a != null){
            a.enabled = true;
        }
    }

    
    void OnLocation1ButtonClick(){
        Map.SetActive(false);
        Cursor.visible = false;
        Screen.lockCursor = true;
        if(a != null){
            a.transform.position = Location1.transform.position;
            a.enabled = true;
        }
    }

    
    void OnLocation2ButtonClick(){
        Map.SetActive(false);
        Cursor.visible = false;
        Screen.lockCursor = true;
        if(a != null){
            a.transform.position = Location2.transform.position;
            a.enabled = true;
        }
    
    }

    void OnLocation3ButtonClick(){
        Map.SetActive(false);
        Cursor.visible = false;
        Screen.lockCursor = true;
        if(a != null){
            a.transform.position = Location3.transform.position;
            a.enabled = true;
        }
    }

    void OnLocation4ButtonClick(){
        Map.SetActive(false);
        Cursor.visible = false;
        Screen.lockCursor = true;
        if(a != null){
            a.transform.position = Location4.transform.position;
            a.enabled = true;
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = true;
        Screen.lockCursor = false;
    }
}

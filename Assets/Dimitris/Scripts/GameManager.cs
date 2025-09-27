using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public PauseManagerDS a;
    public Button Back, Options, ExitToMainMenu, ExitToDesktop, Yes, No, Yes2, No2, X;
    public GameObject PauseMenu, OptionsMenu, AreYouSureYouWantToGoMainMenu, AreYouSureYouWantToQuitGame, InventoryMenu;
    public GameObject Lamp;
    public LayerMask layerMask;
    public GameObject panel;
    public TMP_Text Text;
    //public FirstPersonController PlayerCamera;
    public DSPlayerMovement PlayerCamera;
    public Camera ThirdPersonControllerCamera, MapCamera;
    public GameObject MapCameraPlacement;
    public GameObject TeleportUI;
    public GameObject Player;
    public DSPlayerMovement m;
    private Vector3[] TeleportPositions;
    public Button[] TeleportButtons;
    //public Image Papyrus;

    private float timer = 0f;
    private bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        a = a.GetComponent<PauseManagerDS>();
        Back.onClick.AddListener(OnBackButtonClick);
        Options.onClick.AddListener(OnOptionsButtonClick);
        ExitToMainMenu.onClick.AddListener(OnExitToMainMenuButtonClick);
        ExitToDesktop.onClick.AddListener(OnExitToDesktopButtonClick);
        Yes.onClick.AddListener(OnYesButtonClick);
        No.onClick.AddListener(OnNoButtonClick);
        Yes2.onClick.AddListener(OnYes2ButtonClick);
        No2.onClick.AddListener(OnNo2ButtonClick);
        X.onClick.AddListener(OnXButtonClick);
        panel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        timer = 0f;
        isMoving = false;
        //Debug.Log(TeleportButtons.Length);
        TeleportPositions = new Vector3[TeleportButtons.Length];
        for (int i = 0; i < TeleportButtons.Length; i++)
        {
            TeleportButtons[i].onClick.AddListener(TeleportB);
        }
    }

    private void OnBackButtonClick()
    {
        a.UnPauseGame();
        PauseMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
    private void OnOptionsButtonClick()
    {
        OptionsMenu.SetActive(true);
        PauseMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
    private void OnExitToMainMenuButtonClick()
    {
        PauseMenu.SetActive(false);
        AreYouSureYouWantToGoMainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);

    }
    private void OnExitToDesktopButtonClick()
    {
        PauseMenu.SetActive(false);
        AreYouSureYouWantToQuitGame.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnYesButtonClick()
    {
        a.UnPauseGame();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        EventSystem.current.SetSelectedGameObject(null);
        Application.LoadLevel("MainMenu");
    }

    private void OnNoButtonClick()
    {
        PauseMenu.SetActive(true);
        AreYouSureYouWantToGoMainMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnYes2ButtonClick()
    {
        Application.Quit();
    }

    private void OnNo2ButtonClick()
    {
        PauseMenu.SetActive(true);
        AreYouSureYouWantToQuitGame.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnXButtonClick()
    {
        InventoryMenu.SetActive(false);
        a.UnPauseGame();
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void TeleportB()
    {
        //Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(MapCamera, GetComponent<RectTransform>());
        //screenPos.z = 10f;
        //Ray ray = MapCamera.ScreenPointToRay(Input.mousePosition);
        Ray ray = MapCamera.ScreenPointToRay(EventSystem.current.currentSelectedGameObject.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 worldPosition = hit.point;
            //Debug.Log("Mouse Clicked at world position: " + worldPosition);
            ThirdPersonControllerCamera.gameObject.transform.position = worldPosition;
            Player.transform.position = worldPosition;
            /*
            float othertimer = 0f;
            while (othertimer < 1f)
            {
                othertimer += Time.unscaledDeltaTime;
                float t = othertimer / 2f;
                MapCamera.gameObject.transform.position = Vector3.Lerp(MapCameraPlacement.transform.position, worldPosition, t);
                Debug.Log("Camera position " + MapCamera.gameObject.transform.position);
                //MapCamera.gameObject.transform.rotation = Quaternion.Lerp(ThirdPersonControllerCamera.gameObject.transform.rotation, MapCameraPlacement.transform.rotation, t);
            }
            */

        }
        /*
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, rectTransform.position);
        */
        //Debug.Log("Position of Button:" + screenPos);

        //Vector3 Position = MapCamera.ScreenToWorldPoint(screenPos);
        //Debug.Log("World position from button: " + Position);

        //ThirdPersonControllerCamera.gameObject.transform.position = Position;
        //Player.transform.position = Position;

        timer = 0f;
        a.UnPauseGame();
        TeleportUI.gameObject.SetActive(false);
        isMoving = false;
        Player.SetActive(true);
        MapCamera.gameObject.SetActive(false);
        ThirdPersonControllerCamera.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0f)
        {
            MapCamera.gameObject.transform.position = ThirdPersonControllerCamera.transform.position;
            //container.transform.rotation = ThirdPersonControllerCamera.transform.rotation;
            if (InputManager.instance.OpenPauseMenuButton)
            {
                //Papyrus.gameObject.SetActive(true);
                PauseMenu.SetActive(true);
                a.PauseGame();
            }
            else if (InputManager.instance.InventoryButton)
            {
                InventoryMenu.SetActive(true);
                a.PauseGame();
            }
            else if (InputManager.instance.MapButton)
            {
                ThirdPersonControllerCamera.gameObject.SetActive(false);
                MapCamera.gameObject.SetActive(true);
                //mapanimator.Play("OpenMapAnimation");
                isMoving = true;
                timer = 0f;
                a.PauseGame();
            }
        }
        else
        {
            //Papyrus.gameObject.SetActive(false);
            if (isMoving == true)
            {
                timer += Time.unscaledDeltaTime;
                float t = timer / 2f;
                MapCamera.gameObject.transform.position = Vector3.Lerp(ThirdPersonControllerCamera.gameObject.transform.position, MapCameraPlacement.transform.position, t);
                MapCamera.gameObject.transform.rotation = Quaternion.Lerp(ThirdPersonControllerCamera.gameObject.transform.rotation, MapCameraPlacement.transform.rotation, t);
                if (t >= 1f)
                {
                    /*
                    for (int i = 0; i < TeleportButtons.Length; i++)
                    {
                        TeleportPositions[i] = MapCamera.ScreenToWorldPoint(TeleportButtons[i].gameObject.transform.position);
                        Debug.Log("Teleport #" + i + ":\t" + TeleportPositions[i].x + "\t" + TeleportPositions[i].y + "\t" + TeleportPositions[i].z);
                    }
                    */
                    Player.SetActive(false);
                    isMoving = false;
                    TeleportUI.SetActive(true);
                }
            }
            else
            {
                if (InputManager.instance.MapButton || InputManager.instance.BackButton)
                {
                    timer = 0f;
                    a.UnPauseGame();
                    TeleportUI.gameObject.SetActive(false);
                    isMoving = false;
                    Player.SetActive(true);
                    MapCamera.gameObject.SetActive(false);
                    ThirdPersonControllerCamera.gameObject.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(null);
                }
            }
            if (PauseMenu.activeSelf)
            {
                if ((InputManager.instance.UpButton || InputManager.instance.DownButton) && EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(Back.gameObject);
                }
            }
            else if (AreYouSureYouWantToGoMainMenu.activeSelf)
            {
                if (InputManager.instance.LeftButton && EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(Yes.gameObject);
                }
                else if (InputManager.instance.RightButton && EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(No.gameObject);
                }
            }
            else if (AreYouSureYouWantToQuitGame.activeSelf)
            {
                if (InputManager.instance.LeftButton && EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(Yes2.gameObject);
                }
                else if (InputManager.instance.RightButton && EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(No2.gameObject);
                }
            }
        }
        

    }


/*
    void FixedUpdate()
    {
        RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, 2f, layerMask))
        {
            // It hit something that is Interractable or within the layermask.
            //Debug.DrawRay(PlayerCamera.transform.position, PlayerCamera.transform.forward * hit.distance, Color.yellow);
            //Debug.Log(hit.transform.name + "Did Hit");
            //Debug.Log(hit.collider.gameObject.name);

            panel.transform.position = new Vector3(
                    hit.collider.gameObject.transform.position.x + 1,
                    //PlayerCamera.transform.position.x-PlayerCamera.transform.position.x/2,
                    hit.collider.gameObject.transform.position.y + 1,
                    hit.collider.gameObject.transform.position.z
            );
            panel.transform.rotation = PlayerCamera.transform.rotation;
            panel.transform.localScale = new Vector3(
                hit.collider.gameObject.transform.localScale.x * 1 / 1000,
                hit.collider.gameObject.transform.localScale.y * 1 / 1000,
                hit.collider.gameObject.transform.localScale.z * 1 / 1000
            );

            if (hit.collider.gameObject == Lamp)
            {
                //Debug.Log(true);
                // Get A Menu to open up :D
                Text.text = "Press E to Interract with this object";
                panel.SetActive(true);
                // Then check if the player wishes to interract with it.
                if (Input.GetKeyDown(InterractKey))
                {
                    //Debug.Log("You Have Interracted with this Object.");
                }
            }
            else
            {
                //Debug.Log(false);
                panel.SetActive(false);
            }
        }
        else
        {
            // It didn't hit something that is Interractable or within the layermask.
            Debug.DrawRay(PlayerCamera.transform.position, PlayerCamera.transform.forward * 1000, Color.white);
            //Debug.Log("Did not Hit");
            panel.SetActive(false);
        }
    }
    */
    }



/*
using UnityEngine;

public class TavernLightTesting : MonoBehaviour
{

    public LayerMask layerMask;
    public FirstPersonController a;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(a.transform.position, a.transform.forward, out hit, 100f, layerMask))
        {
            Debug.DrawRay(a.transform.position, a.transform.forward * hit.distance, Color.yellow);
            Debug.Log(hit.transform.name + "Did Hit");
        }
        else
        {
            Debug.DrawRay(a.transform.position, a.transform.forward * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
/*
public class ExampleClass : MonoBehaviour
{
    LayerMask layerMask;
    
    void Awake()
    {
        layerMask = LayerMask.GetMask("Wall", "Character");
    }
                        
    // See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries
    void FixedUpdate()
    {

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))

        { 
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow); 
            Debug.Log("Did Hit"); 
        }
        else
        { 
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white); 
            Debug.Log("Did not Hit"); 
        }

    }
}
*/


using Unity.AppUI.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.EventSystems;
using UnityEditor;

public class GameManagerforMainMenu : MonoBehaviour
{
    public UnityEngine.UI.Button start, Options, Credits, QuitGame, Back, Yes, No;
    public GameObject MainMenu, OptionsMenu, CreditsMenu, ExitMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // for every button we have we are going to add a function that handles the action for every time the use clicks on the button

        if (start != null && Options != null && Credits != null && QuitGame != null && Back != null && Yes != null && No != null)
        {
            start.onClick.AddListener(StartButton);
            Options.onClick.AddListener(OptionsButton);
            Credits.onClick.AddListener(CreditsButton);
            QuitGame.onClick.AddListener(QuitGameButton);
            Back.onClick.AddListener(BackButton);
            Yes.onClick.AddListener(YesButton);
            No.onClick.AddListener(NoButton);
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            Debug.LogError("Some Buttons were not assigned. Please go and assign them.");
        }
    }

    void StartButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        Application.LoadLevel("Game"); // This works too but its obsolete (you dont need to unlaod a level, it does that automatically)
        //Scene Scene = SceneManager.GetActiveScene(); // Outputs the current active Scene to the variable
        //SceneManager.UnloadSceneAsync(Scene);
        //SceneManager.LoadScene("Game", LoadSceneMode.Additive);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));

    }

    void OptionsButton()
    {
        if (MainMenu != null && OptionsMenu != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            MainMenu.SetActive(false);
            OptionsMenu.SetActive(true);
            //EventSystem.current.SetSelectedGameObject(option);
            //this.gameObject.SetActive(false);
            
        }
        else
        {
            Debug.LogError("Some GameObjects are not assigned. Please go and assign them.");
        }
    }

    void CreditsButton()
    {
        if (MainMenu != null && CreditsMenu != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            MainMenu.SetActive(false);
            CreditsMenu.SetActive(true);
        }
        else
        {
            Debug.LogError("Some GameObjects are not assigned. Please go and assign them.");
        }
    }

    void QuitGameButton()
    {
        if (MainMenu != null && ExitMenu != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            MainMenu.SetActive(false);
            ExitMenu.SetActive(true);
        }
        else
        {
            Debug.LogError("Some GameObjects are not assigned. Please go and assign them.");
        }
    }
    void BackButton()
    {
        if (MainMenu != null && CreditsMenu != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            CreditsMenu.SetActive(false);
            MainMenu.SetActive(true);
        }
        else
        {
            Debug.LogError("Some GameObjects are not assigned. Please go and assign them.");
        }
    }

    void YesButton()
    {
        Application.Quit();
    }


    void NoButton()
    {
        if (MainMenu != null && ExitMenu != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            ExitMenu.SetActive(false);
            MainMenu.SetActive(true);
        }
        else
        {
            Debug.LogError("Some GameObjects are not assigned. Please go and assign them.");
        }
    }

    // Update is called once per frame

    void FixedUpdate()
    {

        if (MainMenu.activeSelf)
        {
            if ((InputManager.instance.UpButton || InputManager.instance.DownButton) && EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(start.gameObject);
            }
            /*
            if (InputManager.instance.BackButton)
            {
                EventSystem.current.SetSelectedGameObject(null);
                MainMenu.SetActive(false);
                ExitMenu.SetActive(true);
            }
            */
        }
        else if (CreditsMenu.activeSelf)
        {
            if ((InputManager.instance.UpButton || InputManager.instance.DownButton || InputManager.instance.LeftButton || InputManager.instance.RightButton) && EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(Back.gameObject);
            }
            /*
            if (InputManager.instance.BackButton)
            {
                EventSystem.current.SetSelectedGameObject(null);
                CreditsMenu.SetActive(false);
                MainMenu.SetActive(true);
            }
            */
        }
        else if (ExitMenu.activeSelf)
        {
            if (InputManager.instance.LeftButton && EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(Yes.gameObject);
            }
            else if (InputManager.instance.RightButton && EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(No.gameObject);
            }
            /*
            if (InputManager.instance.BackButton)
            {
                EventSystem.current.SetSelectedGameObject(null);
                MainMenu.SetActive(true);
                ExitMenu.SetActive(false);
            }
            */
        }
        
        //if (InputManager.instance.UpButton)
            //{

            //}
            /*
            if (InputManager.instance.BackButton)
            {
                if (MainMenu.activeSelf)
                {
                    MainMenu.SetActive(false);
                    ExitMenu.SetActive(true);
                }
                else if (ExitMenu.activeSelf)
                {
                    MainMenu.SetActive(true);
                    ExitMenu.SetActive(false);
                }
                else if (CreditsMenu.activeSelf)
                {
                    CreditsMenu.SetActive(false);
                    MainMenu.SetActive(true);
                }
                else if (OptionsMenu.activeSelf)
                {
                    OptionsMenu.SetActive(false);
                    MainMenu.SetActive(true);
                }
            }
            */

            /*
                    else if (InputManager.instance.EnterButton)
                    {

                        if (MainMenu.activeSelf)
                        {
                            MainMenu.SetActive(false);
                            ExitMenu.SetActive(true);
                        }
                        else if (ExitMenu.activeSelf)
                        {
                            MainMenu.SetActive(true);
                            ExitMenu.SetActive(false);
                        }
                        else if (CreditsMenu.activeSelf)
                        {
                            CreditsMenu.SetActive(false);
                            MainMenu.SetActive(true);
                        }
                        else if (OptionsMenu.activeSelf)
                        {
                            OptionsMenu.SetActive(false);
                            MainMenu.SetActive(true);
                        }

                    }
                    */

            /*
            else if (InputManager.instance.UpButton)
            {
                if (MainMenu.activeSelf)
                {
                    if (EventSystem.current.currentSelectedGameObject == st)
                    {
                        EventSystem.current.SetSelectedGameObject(qu);
                    }
                    else if (EventSystem.current.currentSelectedGameObject == op)
                    {
                        EventSystem.current.SetSelectedGameObject(st);
                    }
                    else if (EventSystem.current.currentSelectedGameObject == cr)
                    {
                        EventSystem.current.SetSelectedGameObject(op);
                    }
                    else if (EventSystem.current.currentSelectedGameObject == qu)
                    {
                        EventSystem.current.SetSelectedGameObject(cr);
                    }
                    else
                    {
                        EventSystem.current.SetSelectedGameObject(st);
                    }
                }
            }
            else if (InputManager.instance.DownButton)
            {
                if (MainMenu.activeSelf)
                {
                    if (EventSystem.current.currentSelectedGameObject == st)
                    {
                        EventSystem.current.SetSelectedGameObject(op);
                    }
                    else if (EventSystem.current.currentSelectedGameObject == op)
                    {
                        EventSystem.current.SetSelectedGameObject(cr);
                    }
                    else if (EventSystem.current.currentSelectedGameObject == cr)
                    {
                        EventSystem.current.SetSelectedGameObject(qu);
                    }
                    else if (EventSystem.current.currentSelectedGameObject == qu)
                    {
                        EventSystem.current.SetSelectedGameObject(st);
                    }
                    else
                    {
                        EventSystem.current.SetSelectedGameObject(st);
                    }
                }
            }
            else if (InputManager.instance.LeftButton)
            {
                if (ExitMenu.activeSelf)
                {
                    if (EventSystem.current.currentSelectedGameObject == ye)
                    {
                        EventSystem.current.SetSelectedGameObject(no);
                    }
                    else
                    {
                        EventSystem.current.SetSelectedGameObject(ye);
                    }
                }
            }
            else if (InputManager.instance.RightButton)
            {
                if (ExitMenu.activeSelf)
                {
                    if (EventSystem.current.currentSelectedGameObject == no)
                    {
                        EventSystem.current.SetSelectedGameObject(ye);
                    }
                    else
                    {
                        EventSystem.current.SetSelectedGameObject(no);
                    }
                }
            }
            */
        }
}

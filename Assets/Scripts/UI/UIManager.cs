﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header("Menus"), SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject optionsMenu;
    [SerializeField]
    private GameObject creditsMenu;
    [SerializeField]
    private GameObject levelUI;
    [SerializeField]
    private GameObject characterSelect;
    [Header("Menu Prefabs"), SerializeField]
    private GameObject mainMenuPrefab;
    [SerializeField]
    private GameObject pausePrefab;
    [SerializeField]
    private GameObject optionsPrefab;
    [SerializeField]
    private GameObject creditsPrefab;
    [SerializeField]
    private GameObject levelUIPrefab;
    [SerializeField]
    private GameObject characterSelectPrefab;
    [SerializeField]
    public static bool isPaused;
    [Header("Main menu Contents List"), SerializeField]
    private List<GameObject> mainMenuContents;

    [Header("Scene Attributes"), SerializeField]
    private int mainMenuIndex = 1;
    [SerializeField]
    private int levelIndex = 3;
    [Header("Level Attributes")]
    [SerializeField]
    private Image playerHealthBar;
    [SerializeField]
    private Text playerHealthText;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) // if instance is empty
        {
            instance = this; // store THIS instance of the class in the instance variable
            DontDestroyOnLoad(this.gameObject); //keep this instance of game manager when loading new scenes
        }
        else
        {
            Destroy(this.gameObject); // delete the new game manager attempting to store itself, there can only be one.
            Debug.Log("Warning: A second game manager was detected and destrtoyed"); // display message in the console to inform of its demise
        }
        //check for missing menu objects at start
        CheckMenuObjects();
    }

    // Update is called once per frame
    void Update()
    {
        //check to see if we are not at our main menu
        if (SceneManager.GetActiveScene().buildIndex == levelIndex)
        {
            //and if the player is pressing down the escape key
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //if the pause menu is not active
                if (UIManager.isPaused)
                {
                    Resume();
                }
                //otherwise
                else
                {
                    Pause();
                }
            }
        }

        //check for missing menu objects
        CheckMenuObjects();
        if (LevelManager.instance.isGameStart == true) 
        {
            levelUI.SetActive(true);
        }
    }

    public void CloseAllMenus() 
    {
        if (characterSelect.activeSelf == true)
        {
            characterSelect.SetActive(false);
        }

        if (mainMenu.activeSelf == true)
        {
            //set main menu to inactive
            mainMenu.SetActive(false);
        }
    }

    public void OpenCharacterSelect() 
    {
        //if the main menu is active
        if (mainMenu.activeSelf == true)
        {
            //set main menu to inactive
            mainMenu.SetActive(false);
        }

        if (characterSelect.activeSelf == false)
        {
            characterSelect.SetActive(true); 
        }
    }
    public void CloseCharacterSelect()
    {
        if (characterSelect.activeSelf == true)
        {
            characterSelect.SetActive(false);
        }
        //if the main menu is active
        if (mainMenu.activeSelf == false)
        {
            //set main menu to inactive
            mainMenu.SetActive(true);
        }
        if (optionsMenu.activeSelf == true) 
        {
            //set the options menu to inactive
            optionsMenu.SetActive(false);
        }
        if (creditsMenu.activeSelf == true) 
        {
            //set the credits menu to inactive
            creditsMenu.SetActive(false);
        }
        if (pauseMenu.activeSelf == true) 
        {
            pauseMenu.SetActive(false);
        }
    }

    public void OpenOptionsMenu() 
    {
        //if we are at the main menu scene
        if (SceneManager.GetActiveScene().buildIndex == mainMenuIndex)
        {
            mainMenu.SetActive(false);
        }
        //if we are in our level scene
        if (SceneManager.GetActiveScene().buildIndex == levelIndex)
        {
            //set the pause menu active again
            pauseMenu.SetActive(false);
        }
        //set the options menu to active
        optionsMenu.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        //set the options menu to inactive
        optionsMenu.SetActive(false);

        //if we are at the main menu scene
        if (SceneManager.GetActiveScene().buildIndex == mainMenuIndex)
        {
            mainMenu.SetActive(true);
        }
        //if we are in our level scene
        if (SceneManager.GetActiveScene().buildIndex == levelIndex) 
        {
            //set the pause menu active again
            pauseMenu.SetActive(true);
        }
    }
    public void OpenCreditsMenu()
    {
        //if the main menu is active
        if (mainMenu.activeSelf == true)
        {
            //set main menu to inactive
            mainMenu.SetActive(false);
        }
        //set the credits menu to active
        creditsMenu.SetActive(true);
    }

    public void CloseCreditsMenu()
    {
        //set the credits menu to inactive
        creditsMenu.SetActive(false);
        //set main menu to active
        mainMenu.SetActive(true);
    }

    private void CheckMenuObjects() 
    {
        if (!mainMenu) 
        {
            mainMenu = Instantiate(mainMenuPrefab, this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
            mainMenu.SetActive(false);
        }
        if (!pauseMenu)
        {
            pauseMenu = Instantiate(pausePrefab, this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
            pauseMenu.SetActive(false);
        }
        if (!optionsMenu) 
        {
            optionsMenu = Instantiate(optionsPrefab, this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
            optionsMenu.SetActive(false);
        }
        if (!creditsMenu) 
        {
            creditsMenu = Instantiate(creditsPrefab, this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
            creditsMenu.SetActive(false);
        }
        if (!characterSelect) 
        {
            characterSelect = Instantiate(characterSelectPrefab, this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
            characterSelect.SetActive(false);
        }
        if (!levelUI) 
        {
            levelUI = Instantiate(levelUIPrefab, this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
            levelUI.SetActive(false);
        }
    }

    public void EnableMainMenu() 
    {
        mainMenu.SetActive(true); 
    }

    public void LoadNextScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevel() 
    {
        if (characterSelect.activeSelf == true)
        {
            characterSelect.SetActive(false);
        }
        SceneManager.LoadScene(levelIndex);
    }

    public void Pause()
    {
        //activate the pause menu
        pauseMenu.SetActive(true);
        //set time scale to zero
        Time.timeScale = 0f;
        //tell the game its paused
        isPaused = true;
    }

    public void Resume() 
    {
        //deactivate the pause menu
        pauseMenu.SetActive(false);
        //set time scale to 1
        Time.timeScale = 1f;
        //tell game its not paused
        isPaused = false;
    }
    public void RegisterPlayerHealth(PriestPawn priest)
    {
        float healthPercent = priest.GetHealth().GetPercent();
        //set the fill amount
        playerHealthBar.fillAmount = healthPercent;
        //set the text to display the percentage of the player's health
        playerHealthText.text = string.Format("Health: {0}%", Mathf.RoundToInt(healthPercent * 100f));
    }

    public void ReturnToMain() 
    {
        //deactivate the pause menu
        pauseMenu.SetActive(false);
        //set time scale to 1
        Time.timeScale = 1f;
        //tell game its not paused
        isPaused = false;
        //load the main mneu build index
        SceneManager.LoadScene(mainMenuIndex);
        //enable main menu
        EnableMainMenu();
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    public void SetMenusInactive() 
    {
        if (mainMenu.activeSelf == true)
        {
            mainMenu.SetActive(false); 
        }
        if (pauseMenu.activeSelf == true)
        {
            pauseMenu.SetActive(false); 
        }
        if (creditsMenu.activeSelf == true)
        {
            creditsMenu.SetActive(false); 
        }
    }

    public GameObject GetLevelUi() 
    {
        return levelUI;
    }
}

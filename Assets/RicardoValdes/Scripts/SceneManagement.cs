using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour //Handles Scene UI both in-game and in main menu
{
    private static SceneManagement _sceneManagementInstance;
    public static SceneManagement SceneManagementInstance { get { return _sceneManagementInstance; } }

    [Header("Inputs")]
    [SerializeField] Animator sceneTransition;
    [SerializeField] TextMeshProUGUI featureMessage;
    [SerializeField] AudioSource musicSource;
    [SerializeField] public GameObject game;
    [SerializeField] public GameObject gameSettingsButton;
    [SerializeField] public MainMenu mainMenu;
    [SerializeField] public SettingsMenu settingsMenu;
    [SerializeField] string[] featureMessages;

    [Header("Variables")]
    [SerializeField] public int currentScene = 0;
    [SerializeField] public bool isInSettings;
    [SerializeField] public bool hasRunGame = false;

    private void Awake()
    {
        if (_sceneManagementInstance && _sceneManagementInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _sceneManagementInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        UpdateInputs();
    }

    #region CheckInputs
    private void UpdateInputs()
    {
        switch (currentScene)
        {
            case 0:
             
                if (!sceneTransition)
                {
                    sceneTransition = GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<Animator>();
                    Debug.LogWarning("You forgot to set the SceneTransition Animator in the inspector!");
                }

                if (!settingsMenu)
                {
                    settingsMenu = GameObject.FindGameObjectWithTag("SettingsMenu").GetComponent<SettingsMenu>();
                    Debug.LogWarning("You forgot to set the SettingsMenu script in the inspector!");
                }

                if (!gameSettingsButton)
                {
                    gameSettingsButton = GameObject.FindGameObjectWithTag("GameSettingsButton");
                    Debug.LogWarning("You forgot to set the GameSettingsButton Object in the inspector!");
                }

                featureMessage.text = featureMessages[Random.Range(0, featureMessages.Length)]; //Ricardo Dec. 8: Randomly selects a message as the feature message.
                gameSettingsButton.SetActive(false);
                break;

            case 1:
                gameSettingsButton.SetActive(true);
                game = Game.GameInstance.gameObject;
                game.SetActive(true);
                break;

            case 2:
                //functionality checks for 3D Scene
                break;
        }
    }
    #endregion CheckInputs

    public IEnumerator LoadDesiredScene(int desiredScene)
    {
        currentScene = desiredScene;
        sceneTransition.SetBool("Fade", true);
     
        yield return new WaitForSeconds(0.43f); //Ricardo Dec. 10:  Wait for the length of the transition animation, then load the next scene.
                                                //                  This could have been triggered by an Animation Event instead, but for the sake 
                                                //                  of keeping everything under one function, I opted for this solution instead.
        SceneManager.LoadSceneAsync(desiredScene);

        sceneTransition.SetBool("Fade", false);
        settingsMenu.ShowSettingsMenu(false);
        UpdateInputs();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

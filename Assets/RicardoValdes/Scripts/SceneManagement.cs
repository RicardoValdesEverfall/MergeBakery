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
    [SerializeField] GameManagement gameManager;
    [SerializeField] Animator sceneTransition;
    [SerializeField] TextMeshProUGUI featureMessage;
    [SerializeField] public MainMenu mainMenu;
    [SerializeField] public SettingsMenu settingsMenu;
    [SerializeField] string[] featureMessages;

    [Header("Variables")]
    [SerializeField] public int currentScene = 0;
    [SerializeField] public bool isInSettings;

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

        switch (currentScene)
        {
            case 0: //Main Menu Scene
                
                gameManager.enabled = false;
                break;

            case 1: //2D Game Scene

                gameManager.enabled = true;
                break;
            case 2: //3D Game Scene

                gameManager.enabled = true;
                break;
        }
    }

    void Start()
    {
        #region CheckInputs_LogWarnings

        if (!featureMessage)
        {
            featureMessage = GameObject.FindGameObjectWithTag("FeatureMessage").GetComponent<TextMeshProUGUI>();
            Debug.LogWarning("You forgot to set the FeatureMessage Text Object in the inspector!");
        }

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

        if (!mainMenu)
        {
            mainMenu = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<MainMenu>();
            Debug.LogWarning("You forgot to set the MainMenu script in the inspector!");
        }
        #endregion CheckInputs_LogWarnings

        featureMessage.text = featureMessages[Random.Range(0, featureMessages.Length)]; //Ricardo Dec. 8: Randomly selects a message as the feature message.
    }

    #region Functions
    public IEnumerator LoadDesiredScene(int desiredScene)
    {
        currentScene = desiredScene;
        sceneTransition.SetTrigger("Transition");

        yield return new WaitForSeconds(0.43f); //Ricardo Dec. 10:  Wait for the length of the transition animation, then load the next scene.
                                               //                  This could have been triggered by an Animation Event instead, but for the sake 
                                               //                  of keeping everything under one function, I opted for this solution instead.
        SceneManager.LoadScene(desiredScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour //Handles Scene UI both in-game and in main menu
{
    [Header("Inputs")]
    [SerializeField] GameObject settingsMenuParent;
    [SerializeField] Animator sceneTransition;
    [SerializeField] TextMeshProUGUI featureMessage;
    [SerializeField] string[] featureMessages;

    [Header("Variables")]
    [SerializeField] int currentScene;
    [SerializeField] bool isInSettings;

    void Start()
    {
        if (!featureMessage)
        {
            featureMessage = GameObject.FindGameObjectWithTag("FeatureMessage").GetComponent<TextMeshProUGUI>();
        }

        if (!sceneTransition)
        {
            sceneTransition = GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<Animator>();
        }

        featureMessage.text = featureMessages[Random.Range(0, featureMessages.Length)]; //Ricardo Dec. 8: Randomly selects a message as the feature message.
    }

    void Update()
    {
        
    }

    public void LoadDesiredScene(int desiredScene)
    {
        currentScene = desiredScene;
        //Trigger scene transition
        SceneManager.LoadScene(desiredScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MusicVolume(float volume)
    {

    }

    public void ShowSettingsMenu(bool enableOrDisable) //Ricardo, Dec. 8: True = Enabled, False = Disabled.
    {
        switch (enableOrDisable) //Ricardo, Dec. 8: Using a switch here only because its more aesthetic lol.
        {
            case true:
                //enable settings UI
                //pause
                //play audio
                isInSettings = true;
                return;

            case false:
                //disable settings UI
                //resume
                //play audio
                isInSettings = false;
                return;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI quitToMMButton;

    public AudioSource musicSource;

    public SceneManagement _SMsceneManagement = SceneManagement.SceneManagementInstance;

    private void Start()
    {
        if (!quitToMMButton)
        {
            quitToMMButton = GameObject.FindGameObjectWithTag("QuitToMM").GetComponent<TextMeshProUGUI>();
        }
    }

    public void SetVolume (float vol)
    {
        musicSource.volume = vol;
    }

    public void ChangeResolution()
    {

    }

    public void SetGameTo3D()
    {
        if (_SMsceneManagement.currentScene == 0)
            _SMsceneManagement.mainMenu._gameScene = 2;
        else
        {
            _SMsceneManagement.LoadDesiredScene(2);
        }
    }

    public void QuitToMainMenu()
    {
        _SMsceneManagement.LoadDesiredScene(0);
    }

    public void ShowSettingsMenu(bool enableOrDisable) //Ricardo, Dec. 8: True = Enabled, False = Disabled.
    {
        if (_SMsceneManagement.currentScene == 0)
        {
            quitToMMButton.enabled = false;
        }

        switch (enableOrDisable) //Ricardo, Dec. 8: Using a switch here only because its more aesthetic lol.
        {
            case true:
                gameObject.SetActive(true);
                //pause
                //play audio
                _SMsceneManagement.isInSettings = true;
                return;

            case false:
                gameObject.SetActive(false);
                //resume
                //play audio
                _SMsceneManagement.isInSettings = false;
                return;
        }
    }
}

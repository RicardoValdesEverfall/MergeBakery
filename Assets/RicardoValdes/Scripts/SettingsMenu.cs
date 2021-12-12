using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI quitToMMButton;

    public AudioSource musicSource;
    public SceneManagement _SMsceneManagement;
    public TMP_Dropdown resolutionMenu;

    Resolution[] resolutions;
    private void Start()
    {
        _SMsceneManagement = SceneManagement.SceneManagementInstance;

        if (!quitToMMButton)
        {
            quitToMMButton = GameObject.FindGameObjectWithTag("QuitToMM").GetComponent<TextMeshProUGUI>();
        }

        //Ricardo Dec. 10: This block of code allows the Resolution Dropdown menu inside the Settings Menu to display all available resolutions on a per-system basis.
        resolutions = Screen.resolutions;  
        resolutionMenu.ClearOptions();

        List<string> resOptions = new List<string>();  //List instead of string array because of AddOption() method.

        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string resOption = resolutions[i].width + " x " + resolutions[i].height;
            resOptions.Add(resOption);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) //Unity does not allow for Resolution to compare to Screen.currentResolution.
            {                                                                                                                       //so we have to compare width then height instead.
                currentResIndex = i;
            }
        }
        resolutionMenu.AddOptions(resOptions);
        resolutionMenu.value = currentResIndex;
        resolutionMenu.RefreshShownValue();
    }

    public void SetVolume (float vol)
    {
        musicSource.volume = vol;
    }

    public void ChangeResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, false);
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
        StartCoroutine(_SMsceneManagement.LoadDesiredScene(0));
    }

    public void ShowSettingsMenu(bool enableOrDisable) //Ricardo, Dec. 8: True = Enabled, False = Disabled.
    {
        if (_SMsceneManagement.currentScene == 0)
        {
            quitToMMButton.enabled = false;
        }
        else
        {
            quitToMMButton.enabled = true;
        }

        switch (enableOrDisable) //Ricardo, Dec. 8: Using a switch here only because its more "aesthetic".
        {
            case true:
                gameObject.SetActive(true);
                //pause
                //play SFX audio
                _SMsceneManagement.gameSettingsButton.SetActive(false);
                _SMsceneManagement.isInSettings = true;
                return;

            case false:
                gameObject.SetActive(false);
                //resume
                //play SFX audio
                _SMsceneManagement.gameSettingsButton.SetActive(true);
                _SMsceneManagement.isInSettings = false;
                return;
        }
    }
}

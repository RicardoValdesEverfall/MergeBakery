using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public SceneManagement _MMSceneManagement;

    public int _gameScene = 1; //Ricardo Dec 10: Using this int to change from the 2D game scene or the 3D game scene. Could have made an Enum for extra readability & 'aesthetics', but there's only 2 game scenes.

    private void Start()
    {
        _MMSceneManagement = SceneManagement.SceneManagementInstance;
    }


    public void StartButton()
    {
       StartCoroutine(_MMSceneManagement.LoadDesiredScene(_gameScene));
          
    }

    public void SettingsButton(bool _val)
    {
        _MMSceneManagement.settingsMenu.ShowSettingsMenu(_val);
    }

    public void QuitButton()
    {
        _MMSceneManagement.QuitGame();
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : SingleSceneManager
{
    [SerializeField] private MenuManager mainMenuUIManager;
    [SerializeField] private MenuManager settingsMenuManager;

    private Sound mainMenuThemeSound;

    private void Start()
    {
        mainMenuUIManager.SetActive(true);
        settingsMenuManager.SetActive(false);

        MenuManager.ButtonClicked += OnButtonClicked;

        mainMenuThemeSound = AudioManager.Instance.GetSound("Main menu theme");
        mainMenuThemeSound.SetVolumeToMax();
        mainMenuThemeSound.Play();
    }

    private void OnButtonClicked(ButtonPressedInfo info)
    {
        switch (info.ButtonType)
        {
            case ButtonType.Quit:
                print("Quit");
                Application.Quit();
                break;
            case ButtonType.Back:
                if(info.MenuType == MenuType.SettingsMenu)
                {
                    settingsMenuManager.SetActive(false);
                    mainMenuUIManager.SetActive(true);
                }
                break;
            case ButtonType.Settings:
                OnSettingsBtnClicked();
                break;
            case ButtonType.Play:
                OnPlayBtnClicked();
                break;
            case ButtonType.Pause:
                break;
            case ButtonType.Menu:
                break;
            case ButtonType.Resume:
                break;
            case ButtonType.Continue:
                break;
            case ButtonType.Restart:
                break;
            default:
                break;
        }
    }

    private void OnSettingsBtnClicked()
    {
        mainMenuUIManager.SetActive(false);
        settingsMenuManager.SetActive(true);
    }

    private void OnDestroy()
    {
        MenuManager.ButtonClicked -= OnButtonClicked;
        //mainMenuThemeSound?.Stop();
    }

    private void OnPlayBtnClicked()
    {
        mainMenuThemeSound?.Stop();
        RequestSceneChange(GameState.Level);
    }
}

using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private MenuType menuType;
    [SerializeField] private ButtonOfType[] buttons;
    [SerializeField] private GameObject menu;

    public static event Action<ButtonPressedInfo> ButtonClicked;

    private void Start()
    {
        foreach (var b in buttons)
        {
            b.Button.onClick.AddListener(() => ButtonClicked?.Invoke(new ButtonPressedInfo(b.ButtonType, menuType)));
        }
        OnMenuManagerStart();
    }

    protected virtual void OnMenuManagerStart() { }

    public void SetActive(bool active) => menu.SetActive(active);
}

public enum ButtonType
{
    Quit,
    Back,
    Settings,
    Play,
    Pause,
    Menu,
    Resume,
    Continue,
    Restart
}

public enum MenuType
{
    None,
    MainMenu,
    SettingsMenu,
    PauseMenu,
    GameOverMenu
}

[System.Serializable]
public class ButtonOfType
{
    public Button Button;
    public ButtonType ButtonType;
}

public class ButtonPressedInfo
{
    public readonly ButtonType ButtonType;
    public readonly MenuType MenuType;

    public ButtonPressedInfo(ButtonType buttonType, MenuType menuType)
    {
        ButtonType = buttonType;
        MenuType = menuType;
    }
}
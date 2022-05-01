using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MenuManager
{
    [SerializeField] private Button pauseBtn;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button resumeBtn;

    private bool _paused;
    public bool Paused
    {
        get => _paused;
        private set
        {
            _paused = value;
            PauseStateChanged?.Invoke(_paused);
            pauseMenu.SetActive(_paused);
            pauseBtn.gameObject.SetActive(!_paused);
        }
    }

    private static Button _pauseBtnInstance;

    public static event Action<bool> PauseStateChanged;

    protected override void OnMenuManagerStart()
    {
        _pauseBtnInstance = pauseBtn;
        _paused = false;
        pauseBtn.onClick.AddListener(OnPauseBtnClicked);
        resumeBtn.onClick.AddListener(OnResumeBtnClicked);
    }

    private void OnPauseBtnClicked() => TogglePaused();

    private void OnResumeBtnClicked() => TogglePaused();

    private void TogglePaused() => Paused = !Paused;

    public static void SetPauseBtnActive(bool active) => _pauseBtnInstance?.gameObject?.SetActive(active);
}

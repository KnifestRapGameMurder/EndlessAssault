using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : SingleSceneManager
{
    [SerializeField] private HealthControl playerHealthControl;
    [SerializeField] private HealthDisplay healthDisplay;
    [SerializeField] private KillsScoreDisplay scoreDisplay;

    [SerializeField] private MenuManager gameOverMenu;

    private KillScoreCounter _killScoreCounter;
    private Sound mainThemeSound;

    private void Start()
    {
        healthDisplay.SetMaxHealth(playerHealthControl.MaxHealth);
        healthDisplay.SetHealth(playerHealthControl.Health);
        playerHealthControl.HealthChanged += OnPlayerHealthChanged;

        _killScoreCounter = new KillScoreCounter();
        Enemy.EnemyKilled += OnEnemyKilled;

        MenuManager.ButtonClicked += OnButtonClicked;
        PauseManager.PauseStateChanged += OnPauseStateChanged;

        mainThemeSound = AudioManager.Instance.GetSound("Main theme p1");
        mainThemeSound.SetVolumeToMax();
        mainThemeSound.Play();
        ResumeGame();
    }

    private void OnButtonClicked(ButtonPressedInfo info)
    {
        switch (info.ButtonType)
        {
            case ButtonType.Quit:
                OnQuitGameBtnPressed();
                break;
            case ButtonType.Back:
                break;
            case ButtonType.Settings:
                break;
            case ButtonType.Play:
                break;
            case ButtonType.Pause:
                break;
            case ButtonType.Menu:
                OnMenuBtnClicked();
                break;
            case ButtonType.Resume:
                break;
            case ButtonType.Continue:
                break;
            case ButtonType.Restart:
                OnRestartBtnClicked();
                break;
            default:
                break;
        }
        UpdateBestScore();
    }

    private void OnMenuBtnClicked()
    {
        mainThemeSound?.Stop();
        RequestSceneChange(GameState.MainMenu);
    }

    private void OnEnemyKilled(EnemyKilledInfo obj)
    {
        _killScoreCounter.AddScore(1);
        scoreDisplay.SetScore(_killScoreCounter.Score);
    }

    private void OnPauseStateChanged(bool paused)
    {
        if (paused)
        {
            PauseGame();
            mainThemeSound.SetVolumeToHalf();
        }
        else
        {
            ResumeGame();
            mainThemeSound.SetVolumeToMax();
        }
    }

    private void OnQuitGameBtnPressed()
    {
        print("Quit");
        Application.Quit(); 
    }

    private void OnRestartBtnClicked()
    {
        gameOverMenu.SetActive(false);
        PauseManager.SetPauseBtnActive(true);
        RequestSceneChange(GameState.Level);
    }

    private void PauseGame() => Time.timeScale = 0;

    private void ResumeGame() => Time.timeScale = 1;

    private void OnDestroy()
    {
        UpdateBestScore();
        playerHealthControl.HealthChanged -= OnPlayerHealthChanged;
        Enemy.EnemyKilled -= OnEnemyKilled;
        PauseManager.PauseStateChanged -= OnPauseStateChanged;
        MenuManager.ButtonClicked -= OnButtonClicked;
        mainThemeSound?.Stop();
    }

    private void OnPlayerHealthChanged(float health)
    {
        healthDisplay.SetHealth(health);
        if (health <= 0) OnPlayerDied();
    }

    private void OnPlayerDied()
    {
        UpdateBestScore();
        PauseManager.SetPauseBtnActive(false);
        gameOverMenu.SetActive(true);
    }

    private void UpdateBestScore()
    {
        if(_killScoreCounter.Score > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", _killScoreCounter.Score);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        else Instance = this;

        DontDestroyOnLoad(gameObject);

        CurrentState = GameState.MainMenu;

        SingleSceneManager.ChangingScene += OnChangingScene;
    }


    private void OnChangingScene(GameState state)
    {
        CurrentState = state;
        switch (CurrentState)
        {
            case GameState.MainMenu:
                SceneManager.LoadScene("MainMenuScene");
                break;
            case GameState.Level:
                SceneManager.LoadScene("LevelScene");
                break;
            default:
                break;
        }
    }
}

public enum GameState
{
    MainMenu,
    Level
}
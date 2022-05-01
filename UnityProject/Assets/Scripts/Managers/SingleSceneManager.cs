using System;
using UnityEngine;

public class SingleSceneManager : MonoBehaviour
{
    public static event Action<GameState> ChangingScene;

    protected void RequestSceneChange(GameState gameState) => ChangingScene?.Invoke(gameState);
}

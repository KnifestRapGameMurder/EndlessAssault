using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestScore;
    [SerializeField] private TextMeshProUGUI coins;

    private void Start()
    {
        bestScore.text = "0";
        coins.text = "0";
        SetBestScore(PlayerPrefs.GetInt("BestScore"));
        SetCoins(PlayerPrefs.GetInt("Coins"));
    }

    public void SetBestScore(int score)
    {
        bestScore.text = score.ToString();
    }

    public void SetCoins(int newCoins)
    {
        coins.text = newCoins.ToString();
    }
}

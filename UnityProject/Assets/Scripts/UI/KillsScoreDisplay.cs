using UnityEngine;
using TMPro;

public class KillsScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start() => scoreText.text = "0";

    public void SetScore(int score) => scoreText.text = score.ToString();
}
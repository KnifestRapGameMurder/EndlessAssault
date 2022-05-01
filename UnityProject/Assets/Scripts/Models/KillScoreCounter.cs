internal class KillScoreCounter
{
    private int _score;

    public int Score
    {
        get => _score;
        private set => _score = value < 0 ? 0 : value;
    }

    public KillScoreCounter() => _score = 0;

    public void AddScore(int amount) => Score += amount;
}
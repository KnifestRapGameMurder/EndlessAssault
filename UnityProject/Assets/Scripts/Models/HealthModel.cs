using System;

class HealthModel
{
    private float _maxHealth;
    private float _health;

    public float MaxHealth
    {
        get => _maxHealth;
        private set => _maxHealth = value < 0 ? 0 : value;
    }
    public float Health
    {
        get => _health;
        private set
        {
            _health = value < 0 ? 0 : value > MaxHealth ? MaxHealth : value;
            if (!IsAlive) Died?.Invoke(this, EventArgs.Empty);
        }
    }
    public bool IsAlive => _health > 0;

    public event EventHandler Died;

    public HealthModel(float maxHealth, float health)
    {
        MaxHealth = maxHealth;
        Health = health;
    }

    public void Damage(float hp) => Health -= hp;

    public void Heal(float hp) => Health += hp;
}
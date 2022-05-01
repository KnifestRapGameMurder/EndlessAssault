using System;
using UnityEngine;

public class HealthControl : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private BloodSystem bloodSystem;

    private HealthModel _healthSystem;

    public float MaxHealth => _healthSystem.MaxHealth;
    public float Health => _healthSystem.Health;
    public bool IsAlive => _healthSystem.IsAlive;

    public event Action<DamageInfo> DamageTaken;
    public event Action<float> HealthChanged;
    public event Action<DeathInfo> Killed;

    private void Awake()
    {
        _healthSystem = new HealthModel(maxHealth, maxHealth);
        _healthSystem.Died += OnDeath;
    }

    private void OnDestroy()
    {
        _healthSystem.Died -= OnDeath;
        HealthChanged?.Invoke(0);
    }

    private void OnDeath(object sender, EventArgs e)
    {
        Killed?.Invoke(new DeathInfo());
        Destroy(gameObject);
    }

    public void TakeDamage(float hp, Vector3 damagePoint)
    {
        _healthSystem.Damage(hp);
        bloodSystem.ShowBlood(damagePoint);
        HealthChanged?.Invoke(_healthSystem.Health);
        DamageTaken?.Invoke(new DamageInfo(hp, damagePoint));
    }

    public void Heal(float hp)
    {
        _healthSystem.Heal(hp);
        HealthChanged?.Invoke(_healthSystem.Health);
    }
}

public class DeathInfo
{

}

public class DamageInfo
{
    public readonly float Damage;
    public readonly Vector3 DamagePoint;

    public DamageInfo(float damage, Vector3 damagePoint)
    {
        Damage = damage;
        DamagePoint = damagePoint;
    }
}
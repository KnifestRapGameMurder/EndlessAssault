using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private HealthControl healthControl;
    [SerializeField] private Weapon weapon;

    protected float _moveSpeed => moveSpeed;

    protected Rigidbody2D _rigidbody { get; private set; }
    protected Transform _transform { get; private set; }
    protected string targetTag { get; private set; } = "Player";
    protected Transform _targetTransform { get; private set; }
    protected bool _isTargetAlive => _targetTransform != null;


    public static event Action<EnemyKilledInfo> EnemyKilled;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = transform;
    }

    private void Start()
    {
        var target = GameObject.FindGameObjectWithTag(targetTag);
        if (target) _targetTransform = target.transform;
        healthControl.Killed += OnEnemyKilled;
        OnEnemyStart();
    }

    private void OnEnemyKilled(DeathInfo obj) => EnemyKilled?.Invoke(new EnemyKilledInfo());

    protected virtual void OnEnemyStart() { }

    private void OnDestroy()
    {
        if(UnityEngine.Random.Range(0,2) == 1)
        {
            CoinSpawner.SpawnCoin(_transform.position);
        }
        healthControl.Killed -= OnEnemyKilled;
    }

    private void Update()
    {
        if (!_isTargetAlive) return;
        UpdateRotation();
        UseWeapon();
        Move();
    }

    protected abstract void UpdateRotation();

    protected virtual void UseWeapon() => weapon.Use();

    protected abstract void Move();

    protected void LootAtTarget() => transform.MoveLookAt(_targetTransform.position, rotationSpeed * Time.deltaTime);

    protected void MoveToTarget() => _rigidbody.velocity = (_targetTransform.position - transform.position).normalized * moveSpeed;
}

public class EnemyKilledInfo
{

}
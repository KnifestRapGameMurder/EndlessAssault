using UnityEngine;

public class RenegadeEnemy : Enemy
{
    [SerializeField] private float maxDist;
    [SerializeField] private float visionRadius;

    private Vector2 _moveDir;

    protected override void OnEnemyStart() => _moveDir = (Vector2.right * (Random.Range(0, 10) < 5 ? -1 : 1) + Vector2.up * 0.5f).normalized;

    protected override void UpdateRotation() => LootAtTarget();

    protected override void UseWeapon() => ShootIfVisible();

    private void ShootIfVisible()
    {
        if (_transform.DistanceFrom(_targetTransform.position) > visionRadius) return;
        base.UseWeapon();
    }

    protected override void Move() => MoveSideToSideBack();

    private void MoveSideToSideBack()
    {
        if (_transform.position.x >= maxDist) SetMoveDir(false);
        else if (_transform.position.x <= -maxDist) SetMoveDir(true);
        _rigidbody.velocity = _moveDir * _moveSpeed;
    }

    private void SetMoveDir(bool isPositive) => _moveDir = (Vector2.right * (isPositive ? 1 : -1) + Vector2.up * 0.5f).normalized;
}

using UnityEngine;

public class Shotgun : Gun
{
    [SerializeField] private int bulletsCount;
    [SerializeField] private float directionChange;

    private float _maxDir => 1 + directionChange;
    private float _minDir => 1 - directionChange;
    private float _randomDir => Random.Range(_minDir, _maxDir);

    protected override void Shoot()
    {
        for (int i = 0; i < bulletsCount; i++)
        {
            var bullet = Instantiate(BulletPrefab, _transform.position, _transform.rotation);
            var dir = _transform.up.normalized;
            dir.x *= _randomDir;
            dir.y *= _randomDir;
            bullet.GetComponent<Rigidbody2D>().velocity = dir * BulletVelocity * _randomDir;
        }
    }
}
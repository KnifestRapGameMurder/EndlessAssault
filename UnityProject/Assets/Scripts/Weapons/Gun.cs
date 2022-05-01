using System;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletVelocity;
    [SerializeField] private ParticleSystem bulletCases;
    [SerializeField] private Transform bulletCaseSpawner;

    protected GameObject BulletPrefab => bulletPrefab;
    protected float BulletVelocity => bulletVelocity;

    protected override void OnWeaponUse() { }

    protected override void OnWeaponCanUse()
    {
        Shoot();
        ThrowBulletCase();
    }

    protected virtual void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, _transform.position, _transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = _transform.up.normalized * bulletVelocity;
    }

    private void ThrowBulletCase() => Instantiate(bulletCases, bulletCaseSpawner.position, bulletCaseSpawner.rotation);
}
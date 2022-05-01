using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    [SerializeField] private float radius;
    [SerializeField] private float damage;
    [SerializeField] private string stabSound;

    protected override void OnWeaponUse() { }
    protected override void OnWeaponCanUse() => Heat();

    private void Heat()
    {
        var collider = Physics2D.OverlapCircle(_transform.position, radius);
        if (!collider) return;
        var health = collider.GetComponent<HealthControl>();
        if (!health) return;
        health.TakeDamage(damage, collider.ClosestPoint(_transform.position));
        AudioManager.Instance.PlaySound(stabSound);
    }
}

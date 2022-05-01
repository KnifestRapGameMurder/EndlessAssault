using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private float startRechargeTime;

    protected float rechargeTime;
    protected Transform _transform { get; private set; }

    private void Awake() => _transform = transform;

    private void Start() => rechargeTime = 0;

    public void Use()
    {
        OnWeaponUse();
        if (!CanUse()) return;
        OnWeaponCanUse();
    }

    protected bool CanUse()
    {
        if ((rechargeTime -= Time.deltaTime) > 0) return false;
        else
        {
            rechargeTime = startRechargeTime;
            return true;
        }
    }

    protected abstract void OnWeaponUse();

    protected abstract void OnWeaponCanUse();
}

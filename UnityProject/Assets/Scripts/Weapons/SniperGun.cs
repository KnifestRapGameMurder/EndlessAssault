using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperGun : Gun
{
    [SerializeField] private float laserLength;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform laserTransform;

    protected override void OnWeaponUse() => DrawLaser();

    private void DrawLaser()
    {
        var hit = Physics2D.Raycast(_transform.position, _transform.up);
        DrawLine(laserTransform.position, hit ? hit.point : (Vector2)(laserTransform.position + laserTransform.up * laserLength));
    }

    private void DrawLine(Vector2 origin, Vector2 end)
    {
        lineRenderer.SetPosition(0, origin);
        lineRenderer.SetPosition(1, end);
    }
}

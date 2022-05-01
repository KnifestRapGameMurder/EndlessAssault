using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotterEnemy : Enemy
{
    protected override void Move() { }

    protected override void UpdateRotation() => LootAtTarget();
}

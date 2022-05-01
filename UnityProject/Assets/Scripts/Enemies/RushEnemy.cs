public class RushEnemy : Enemy
{
    protected override void UpdateRotation() => LootAtTarget();

    protected override void Move() => MoveToTarget();
}
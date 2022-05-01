using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ClosestEnemyDetector
{
    public Transform SearchTransform;
    public float SearchRadius;
    public string EnemyTag;
    public string BarricadeTag;

    public float RotationStep { get; private set; }
    public Collider2D Collider { get; private set; }
    public Vector2 Position { get; private set; }
    public Vector2[] SearchDirections { get; private set; }

    public void SetRotationStep(float rotationStep = 10f)
    {
        RotationStep = (rotationStep != 0f ? rotationStep : 10f);
        SearchDirections = new Vector2[Mathf.FloorToInt(360 / RotationStep)];
        Vector2 dir = Vector2.right;
        for (int i = 0; i < SearchDirections.Length; i++)
        {
            SearchDirections[i] = dir;
            dir = Quaternion.Euler(0, 0, RotationStep) * dir;
        }
    }

    public ClosestEnemyDetector() : this(null, 0, "Enemy", "Barricade") { }

    public ClosestEnemyDetector(Transform searchTransform, float searchRadius,
        string enemyTag, string barricadeTag)
    {
        SearchTransform = searchTransform;
        SearchRadius = searchRadius;
        EnemyTag = enemyTag;
        BarricadeTag = barricadeTag;
    }

    public void SearchForEnemies()
    {
        RaycastHit2D closestHit = new RaycastHit2D();
        closestHit.distance = -1;
        for (int i = 0; i < SearchDirections.Length; i++)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(SearchTransform.position, SearchDirections[i], SearchRadius);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.CompareTag(BarricadeTag))
                    break;
                else if (hit.collider.CompareTag(EnemyTag))
                {
                    if ((closestHit.distance == -1) || (hit.distance < closestHit.distance))
                        closestHit = hit;
                }
            }
        }
        if (closestHit.distance != -1)
        {
            Collider = closestHit.collider;
            Position = closestHit.point;
        }
    }
}

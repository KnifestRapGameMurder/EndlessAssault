using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ClosestEnemyDetector enemyDetector;
    [SerializeField] private MoveControl moveControl;

    private Transform t;
    private Rigidbody2D rb;

    private void Awake()
    {
        t = transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        moveControl.T = t;
        moveControl.Rb = rb;
        moveControl.SetCamera(Camera.main);
        enemyDetector.SetRotationStep();
    }

    private void Update()
    {
        for (int i = 0; i < enemyDetector.SearchDirections.Length; i++)
        {
            Debug.DrawRay(t.position, enemyDetector.SearchDirections[i] * enemyDetector.SearchRadius, Color.red);
        }
        enemyDetector.SearchForEnemies();
        if (enemyDetector.Collider != null)
        {
            moveControl.LookAt(enemyDetector.Position);
        }
    }

    private void FixedUpdate()
    {
        moveControl.MoveRB();
    }
}

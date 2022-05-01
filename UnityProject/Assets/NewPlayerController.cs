using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform defaultLookTarget;
    [SerializeField] private Weapon weapon;
    [SerializeField] private Camera mainCamera;

    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Transform _lookTarget;

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _transform.MoveLookAt(_lookTarget.position, rotationSpeed * Time.deltaTime);
    }
}

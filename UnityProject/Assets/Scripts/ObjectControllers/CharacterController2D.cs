using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public Transform LookTarget
    {
        get => _lookTarget ? _lookTarget : defaultLookTarget;
        set => _lookTarget = value;
    }

    [HideInInspector]
    public Vector2 RequiredPosition;

    public void MoveTo(Vector2 position, bool useRigidbody = true)
    {
        if (useRigidbody) _rigidbody.MovePosition(position);
        else _transform.position = position;
    }

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform defaultLookTarget;

    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Transform _lookTarget;

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _lookTarget = defaultLookTarget;
        RequiredPosition = Vector2.zero;
    }

    private void Update()
    {
        _transform.MoveLookAt(LookTarget.position, rotationSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (_rigidbody.position != RequiredPosition) _rigidbody.MovePosition(Vector2.MoveTowards(_rigidbody.position, RequiredPosition, moveSpeed * Time.fixedDeltaTime));
    }
}

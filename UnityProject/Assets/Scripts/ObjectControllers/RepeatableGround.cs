using UnityEngine;

public class RepeatableGround : MonoBehaviour
{
    [SerializeField] private Transform visibleAreaTransform;
    [SerializeField] private float offset;

    private Transform _transform;

    private void Awake() => _transform = transform;

    private void Update() { if (visibleAreaTransform.position.y >= _transform.position.y + offset) _transform.position += Vector3.up * 2 * offset; }
}
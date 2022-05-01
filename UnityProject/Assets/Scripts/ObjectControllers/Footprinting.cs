using UnityEngine;

public class Footprinting : MonoBehaviour
{
    [SerializeField] private ParticleSystem footprints;
    [SerializeField] private float stepTime;

    private Transform _transform;
    private float remainStepTime;

    private void Start()
    {
        _transform = transform;
        remainStepTime = stepTime;
    }

    private void Update()
    {
        if((remainStepTime -= Time.deltaTime) <= 0)
        {
            remainStepTime = stepTime;
            Instantiate(footprints, (Vector2)_transform.position, _transform.rotation);
        }
    }
}
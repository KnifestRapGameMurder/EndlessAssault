using UnityEngine;

public class LevelMoving : MonoBehaviour, ITimeSinceStartDependent
{
    private Transform _transform;

    

    public float TimeSinceStart { get; set; }
    public float MoveSpeedMultiplier { get; set; }
    public float Timeoffset { get; set; }
    public float TimeOffsetByTimeMultiplier { get; set; }
    public float MoveSpeed { get; set; }

    private void Awake() {
        _transform = transform;
    }
    private void Update() => _transform.position += Vector3.up * MoveSpeed * Time.deltaTime;

    //private void FixedUpdate() => _transform.position += Vector3.up * MoveSpeed * Time.fixedDeltaTime;
    
}

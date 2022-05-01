using System.Collections.Generic;
using UnityEngine;

public class LevelSpeedManager : MonoBehaviour
{
    [SerializeField] private LevelMoving levelMoving;
    [SerializeField] private Spawner spawner;
    [SerializeField] private float timeOffset;
    [SerializeField] private float moveSpeedMultiplier;
    [SerializeField] private float addTime;

    private List<ITimeSinceStartDependent> _timeDependentObjects;
    private float _timeSinceStart;
    private float _timeOffsetByTimeMultiplier;
    private float _moveSpeed;

    private void Awake()
    {
        _timeDependentObjects = new List<ITimeSinceStartDependent>();
        _timeDependentObjects.Add(levelMoving);
        _timeDependentObjects.Add(spawner);
    }

    private void Start()
    {
        _timeSinceStart = 0;
        _timeOffsetByTimeMultiplier = timeOffset * Mathf.Pow(1 / moveSpeedMultiplier, 2);
        _moveSpeed = 0;
        foreach (var item in _timeDependentObjects)
        {
            item.TimeSinceStart = _timeSinceStart;
            item.MoveSpeedMultiplier = moveSpeedMultiplier;
            item.Timeoffset = timeOffset;
            item.TimeOffsetByTimeMultiplier = _timeOffsetByTimeMultiplier;
            item.MoveSpeed = _moveSpeed;
        }
    }

    private void Update()
    {
        _timeSinceStart += Time.deltaTime;
        _moveSpeed = Mathf.Sqrt(_timeSinceStart + addTime + _timeOffsetByTimeMultiplier) * moveSpeedMultiplier;
        foreach (var item in _timeDependentObjects)
        {
            item.TimeSinceStart = _timeSinceStart;
            item.MoveSpeed = _moveSpeed;
        }
    }
}
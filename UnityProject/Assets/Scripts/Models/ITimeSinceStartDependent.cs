public interface ITimeSinceStartDependent
{
    public float TimeSinceStart { get; set; }
    public float MoveSpeedMultiplier { get; set; }
    public float Timeoffset { get; set; }
    public float TimeOffsetByTimeMultiplier { get; set; }
    public float MoveSpeed { get; set; }
}
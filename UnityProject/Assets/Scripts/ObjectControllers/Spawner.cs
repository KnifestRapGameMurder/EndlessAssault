using UnityEngine;

public class Spawner : MonoBehaviour, ITimeSinceStartDependent
{
    [SerializeField] private GameObject[] barricades;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] bonuses;
    [SerializeField] private int bonusSpawnChance;
    [SerializeField] private float spawnRadius;
    [SerializeField] private float startTimeBtwSpawn;
    [SerializeField] private float timeDecrementMultiplier;

    private float _timeBtwSpawn;
    private float _reamaimingTimeBtwSpawn;
    private Transform _transform;

    private Quaternion _enemySpawnRotation;

    public float TimeSinceStart { get; set; }
    public float MoveSpeedMultiplier { get; set; }
    public float Timeoffset { get; set; }
    public float TimeOffsetByTimeMultiplier { get; set; }
    public float MoveSpeed { get; set; }


    private void Awake() => _transform = transform;

    private void Start()
    {
        _reamaimingTimeBtwSpawn = 0;
        _enemySpawnRotation = Quaternion.Euler(0, 0, 180);
    }

    private void Update()
    {
        _timeBtwSpawn = startTimeBtwSpawn - MoveSpeed * timeDecrementMultiplier;
        if ((_reamaimingTimeBtwSpawn -= Time.deltaTime) <= 0)
        {
            _reamaimingTimeBtwSpawn = _timeBtwSpawn;
            Spawn();
        }
    }

    private void Spawn()
    {
        int n = Random.Range(0, 100);
        if (n < bonusSpawnChance) SpawnBonus();
        else if (n >= bonusSpawnChance && n < 60) SpawnEnemy();
        else SpawnBarricade();
    }

    private void SpawnEnemy() => Instantiate(GetRandomFromArray(enemies), GetRandomSpawnPosition(), _enemySpawnRotation);

    private void SpawnBarricade() => Instantiate(GetRandomFromArray(barricades), GetRandomSpawnPosition(), Quaternion.identity);
    
    private void SpawnBonus() => Instantiate(GetRandomFromArray(bonuses), GetRandomSpawnPosition(), Quaternion.identity);

    private GameObject GetRandomFromArray(GameObject[] array) => array[Random.Range(0, array.Length)];

    private Vector3 GetRandomSpawnPosition() => _transform.position + Vector3.right * Random.Range(-spawnRadius, spawnRadius);
}
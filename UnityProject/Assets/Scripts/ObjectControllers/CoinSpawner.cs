using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;

    private static GameObject _coinPrefab;

    private void Awake()
    {
        _coinPrefab = coinPrefab;
    }

    public static void SpawnCoin(Vector2 position)
    {
        Instantiate(_coinPrefab, position, Quaternion.identity);
    }
}

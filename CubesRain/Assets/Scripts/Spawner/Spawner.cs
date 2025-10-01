using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private PoolFactory _pool;
    [SerializeField] private float _intervalSpawnCubes;

    private Coroutine _spawningCubes;

    private void OnEnable()
    {
        _spawningCubes = StartCoroutine(SpawningCubes());
    }

    private void OnDisable()
    {
        StopCoroutine(_spawningCubes);
    }

    private IEnumerator SpawningCubes()
    {
        WaitForSeconds delay = new WaitForSeconds(_intervalSpawnCubes);

        while (_intervalSpawnCubes > 0)
        {
            _pool.RealeseCube();

            yield return delay;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerFactory : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Bomb _bombPrefab;

    private Coroutine _spawningCubes;
    private float _intervalSpawnCubes = 1f;

    private Spawner<Cube> _�ubeSpawner;
    private Spawner<Bomb> _bombSpawner;

    public Spawner<Cube> CubeSpawner => _�ubeSpawner;
    public Spawner<Bomb> BombSpawner => _bombSpawner;

    private void Awake()
    {
        _�ubeSpawner = new Spawner<Cube>(_cubePrefab, 10);
        _bombSpawner = new Spawner<Bomb>(_bombPrefab, 10);
    }

    private void OnEnable()
    {
        foreach (var cube in _�ubeSpawner.Pool) 
            cube.Stored += StoringCube;

        foreach (var bomb in _bombSpawner.Pool)
            bomb.Stored += StoringBomb;
        
        _spawningCubes = StartCoroutine(SpawningCubes());
    }

    private void OnDisable()
    {
        foreach (var cube in _�ubeSpawner.Pool)
            cube.Stored -= StoringCube;

        foreach (var bomb in _bombSpawner.Pool)
            bomb.Stored -= StoringBomb;

        StopCoroutine(_spawningCubes);
    }

    private IEnumerator SpawningCubes()
    {
        WaitForSeconds delay = new WaitForSeconds(_intervalSpawnCubes);

        while (_intervalSpawnCubes > 0)
        {
            Cube cube = _�ubeSpawner.Realese();

            if(cube != null)
                RealeseCube(cube);

            yield return delay;
        }
    }

    private void RealeseCube(Cube cube) 
    {
        cube.Init(new Vector3(UnityEngine.Random.Range(transform.position.x-transform.localScale.x, transform.position.x + transform.localScale.x),
                              transform.position.y,
                              UnityEngine.Random.Range(transform.position.z - transform.localScale.z, transform.position.z + transform.localScale.z)));
    }

    public void StoringCube(Cube cube) 
    {
        _�ubeSpawner.Storing(cube);

        Bomb bomb = _bombSpawner.Realese();
        RealeseBomb(bomb, cube.transform.position);
    }

    public void RealeseBomb(Bomb bomb, Vector3 cubePosition) 
    {
        bomb.Init(cubePosition);
    }

    private void StoringBomb(Bomb bomb)
    {
        _bombSpawner.Storing(bomb);
    }
}

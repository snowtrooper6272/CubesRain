using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnersOutputFactory : MonoBehaviour
{
    [SerializeField] private SpawnerFactory _spawners;
    [SerializeField] private Outputter _spawnedCubes;
    [SerializeField] private Outputter _createdCubes;
    [SerializeField] private Outputter _activeCubes;
    [SerializeField] private Outputter _spawnedBombs;
    [SerializeField] private Outputter _createdBombs;
    [SerializeField] private Outputter _activeBombs;

    private SpawnerOutput<Cube> _cubeSpawnerOutput;
    private SpawnerOutput<Bomb> _bombSpawnerOutput;

    private void Start()
    {
        _cubeSpawnerOutput = new SpawnerOutput<Cube>(_spawners.CubeSpawner, _spawnedCubes, _createdCubes, _activeCubes);
        _bombSpawnerOutput = new SpawnerOutput<Bomb>(_spawners.BombSpawner, _spawnedBombs, _createdBombs, _activeBombs);
    }

    private void OnDisable()
    {
        _cubeSpawnerOutput.Exit();
        _bombSpawnerOutput.Exit();
    }
}

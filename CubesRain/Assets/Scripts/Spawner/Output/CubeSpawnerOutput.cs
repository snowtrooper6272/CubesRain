using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawnerOutput : SpawnerOutput<Cube>
{
    [SerializeField] private PoolFactory _pools;

    protected override void SetTrackedSpawner()
    {
        TrackedPool = _pools.CubePool;
    }
}

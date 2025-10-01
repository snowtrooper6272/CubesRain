using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawnerOutput : SpawnerOutput<Bomb>
{
    [SerializeField] private PoolFactory _pools;

    protected override void SetTrackedSpawner()
    {
        TrackedPool = _pools.BombPool;
    }
}

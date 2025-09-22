using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerOutput<Creature> : MonoBehaviour where Creature : MonoBehaviour
{
    private Outputter _spawnedCount;
    private Outputter _createdCount;
    private Outputter _activeCount;

    private Spawner<Creature> _trackedSpawner;

    public SpawnerOutput(Spawner<Creature> trackedSpawner, Outputter spawned, Outputter created, Outputter active) 
    {
        _trackedSpawner = trackedSpawner;
        _spawnedCount = spawned;
        _createdCount = created;
        _activeCount = active;

        _trackedSpawner.ChangedActivedCount += _activeCount.SetValue;
        _trackedSpawner.ChangedSpawnedCount += _spawnedCount.SetValue;

        _createdCount.SetValue(_trackedSpawner.PoolCapacity);
    }

    public void Exit() 
    {
        _trackedSpawner.ChangedActivedCount -= _activeCount.SetValue;
        _trackedSpawner.ChangedSpawnedCount -= _spawnedCount.SetValue;
    }
}

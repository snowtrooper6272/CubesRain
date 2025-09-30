using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerOutput<T> : MonoBehaviour where T : MonoBehaviour
{
    private Outputter _spawnedCount;
    private Outputter _createdCount;
    private Outputter _activeCount;

    private Pool<T> _trackedPool;

    public void Init(Pool<T> trackedPool, Outputter spawned, Outputter created, Outputter active) 
    {
        _trackedPool = trackedPool;
        _spawnedCount = spawned;
        _createdCount = created;
        _activeCount = active;

        _trackedPool.ChangedActivedCount += _activeCount.SetValue;
        _trackedPool.ChangedSpawnedCount += _spawnedCount.SetValue;

        _createdCount.SetValue(_trackedPool.Capacity);
    } 

    public void Exit() 
    {
        _trackedPool.ChangedActivedCount -= _activeCount.SetValue;
        _trackedPool.ChangedSpawnedCount -= _spawnedCount.SetValue;
    }
}

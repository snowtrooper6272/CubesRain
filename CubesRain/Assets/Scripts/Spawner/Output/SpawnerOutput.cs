using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnerOutput<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private Outputter _spawnedCount;
    [SerializeField] private Outputter _createdCount;
    [SerializeField] private Outputter _activeCount;

    protected Pool<T> TrackedPool;

    private void Start()
    {
        SetTrackedSpawner();

        TrackedPool.ChangedActivedCount += _activeCount.SetValue;
        TrackedPool.ChangedSpawnedCount += _spawnedCount.SetValue;

        _createdCount.SetValue(TrackedPool.Capacity);
    }

    private void OnDisable()
    {
        TrackedPool.ChangedActivedCount -= _activeCount.SetValue;
        TrackedPool.ChangedSpawnedCount -= _spawnedCount.SetValue;
    }

    protected abstract void SetTrackedSpawner(); 
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> : MonoBehaviour where T : MonoBehaviour
{
    private int _spawnedCount;

    public event Action<int> ChangedActivedCount;
    public event Action<int> ChangedSpawnedCount;

    public int Capacity { get; private set; }
    public List<T> Storage { get; private set; } = new List<T>(0);

    public void Init(int capacity, T prefab) 
    {
        Capacity = capacity;

        for (int i = 0; i < Capacity; i++)
        {
            T newCreature = Instantiate(prefab);
            newCreature.gameObject.SetActive(false);
            Storage.Add(newCreature);
        }
    }

    public T Release() 
    {
        if (Storage.Count == 0)
            return null;

        T releaseCreature = Storage[Storage.Count - 1];
        releaseCreature.gameObject.SetActive(true);
        Storage.Remove(releaseCreature);

        _spawnedCount++;
        ChangedSpawnedCount?.Invoke(_spawnedCount);
        ChangedActivedCount?.Invoke(Capacity - Storage.Count);

        return releaseCreature;
    }

    public void Storing(T storingCreature)
    {
        storingCreature.gameObject.SetActive(false);
        Storage.Add(storingCreature);
        ChangedActivedCount?.Invoke(Capacity - Storage.Count);
    }
}

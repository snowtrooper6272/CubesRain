using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> : MonoBehaviour where T : MonoBehaviour
{
    private int _spawnedCount;
    private List<T> _storage = new List<T>(0);

    public event Action<int> ChangedActivedCount;
    public event Action<int> ChangedSpawnedCount;

    public int Capacity { get; private set; }
    public int Count => _storage.Count;

    public void Init(int capacity, T prefab) 
    {
        Capacity = capacity;

        for (int i = 0; i < Capacity; i++)
        {
            T newCreature = Instantiate(prefab);
            newCreature.gameObject.SetActive(false);
            _storage.Add(newCreature);
        }
    }

    public T Release() 
    {
        if (_storage.Count == 0)
            return null;

        T releaseCreature = _storage[_storage.Count - 1];
        releaseCreature.gameObject.SetActive(true);
        _storage.Remove(releaseCreature);

        _spawnedCount++;
        ChangedSpawnedCount?.Invoke(_spawnedCount);
        ChangedActivedCount?.Invoke(Capacity - _storage.Count);

        return releaseCreature;
    }

    public void Storing(T storingCreature)
    {
        storingCreature.gameObject.SetActive(false);
        _storage.Add(storingCreature);
        ChangedActivedCount?.Invoke(Capacity - _storage.Count);
    }

    public List<T> GetStorage() 
    {
        return _storage;
    }
}

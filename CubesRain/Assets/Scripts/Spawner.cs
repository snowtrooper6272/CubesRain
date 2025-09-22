using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner<Creature> : MonoBehaviour where Creature : MonoBehaviour
{
    private int _poolCapacity;
    private int _spawnedCount;

    public event Action<int> ChangedActivedCount;
    public event Action<int> ChangedSpawnedCount;

    public List<Creature> Pool { get; private set; } = new List<Creature>(0);
    public int PoolCapacity => _poolCapacity;

    public Spawner(Creature prefab, int capacity) 
    {
        _poolCapacity = capacity;

        for (int i = 0; i < _poolCapacity; i++) 
        {
            Creature newCreature = Instantiate(prefab);
            newCreature.gameObject.SetActive(false);
            Pool.Add(newCreature);
        }
    }

    public Creature Realese() 
    {
        if (Pool.Count == 0)
            return null;

        Creature realeseCreature = Pool[Pool.Count - 1];
        Pool.Remove(realeseCreature);

        realeseCreature.gameObject.SetActive(true);

        _spawnedCount++;
        ChangedSpawnedCount?.Invoke(_spawnedCount);
        ChangedActivedCount?.Invoke(_poolCapacity - Pool.Count);

        return realeseCreature;
    }

    public void Storing(Creature storingCreature) 
    {
        storingCreature.gameObject.SetActive(false);
        Pool.Add(storingCreature);

        ChangedActivedCount?.Invoke(_poolCapacity - Pool.Count);
    }
}

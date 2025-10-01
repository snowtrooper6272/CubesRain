using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolFactory : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Bomb _bombPrefab;

    private Pool<Cube> _ñubePool;
    private Pool<Bomb> _bombPool;

    public Pool<Cube> CubePool => _ñubePool;
    public Pool<Bomb> BombPool => _bombPool;

    private void Awake()
    {
        _ñubePool = new Pool<Cube>();
        _bombPool = new Pool<Bomb>();

        _ñubePool.Init(10, _cubePrefab);
        _bombPool.Init(10, _bombPrefab);
    }

    private void OnEnable()
    {
        foreach (var cube in _ñubePool.GetStorage()) 
            cube.Stored += Storing;

        foreach (var bomb in _bombPool.GetStorage())
            bomb.Stored += Storing;
    }

    private void OnDisable()
    {
        foreach (var cube in _ñubePool.GetStorage())
            cube.Stored -= Storing;

        foreach (var bomb in _bombPool.GetStorage())
            bomb.Stored -= Storing;
    }

    public void RealeseBomb(Vector3 cubePosition) 
    {
        Bomb bomb = _bombPool.Release();

        if(bomb != null)
            bomb.Init(cubePosition);
    }

    public void RealeseCube()  
    {
        Cube cube = _ñubePool.Release();

        if (cube != null)
        {
            cube.Init(new Vector3(UnityEngine.Random.Range(transform.position.x - transform.localScale.x, transform.position.x + transform.localScale.x),
                                  transform.position.y,
                                  UnityEngine.Random.Range(transform.position.z - transform.localScale.z, transform.position.z + transform.localScale.z)));
        }
    }

    private void StoringCube(Cube cube)
    {
        _ñubePool.Storing(cube);
        RealeseBomb(cube.transform.position);
    }

    private void StoringBomb(Bomb bomb)
    {
        _bombPool.Storing(bomb);
    }

    private void Storing(SpawnObject storingObj) 
    {
        if (storingObj is Cube)
            StoringCube(storingObj.GetComponent<Cube>());
        else if (storingObj is Bomb)
            StoringBomb(storingObj.GetComponent<Bomb>());
    }
}

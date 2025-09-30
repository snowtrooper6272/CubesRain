using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] protected MeshRenderer _renderer;
    [SerializeField] protected int _MinLifeTime = 2;
    [SerializeField] protected int _MaxLifeTime = 5;

    protected float _LifeDuration;
    protected Coroutine _DecreaseTimeOfBackPool; 

    public event Action<SpawnObject> Stored;

    private void OnDisable()
    {
        StopCoroutine(_DecreaseTimeOfBackPool);
    }

    virtual public void Init(Vector3 position) 
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        _renderer.material.color = new Color(0, 0, 0, 1);
        _LifeDuration = UnityEngine.Random.Range(_MinLifeTime, _MaxLifeTime);
    }

    protected void StartLife() 
    {
        _DecreaseTimeOfBackPool = StartCoroutine(DecreaseTimeOfBackPool());
    }

    virtual protected IEnumerator DecreaseTimeOfBackPool() 
    {
        float currentLifeDuration = 0;

        while (currentLifeDuration < _LifeDuration)
        {
            currentLifeDuration += Time.deltaTime;

            CoroutineUpdate(currentLifeDuration);

            yield return null;
        }

        CoroutineEnd();
    }

    virtual protected void CoroutineUpdate(float currentTime) { }

    virtual protected void CoroutineEnd() 
    {
        Stored.Invoke(this);
    }
}

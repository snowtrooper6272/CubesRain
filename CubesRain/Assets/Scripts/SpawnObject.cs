using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] protected MeshRenderer Renderer;
    [SerializeField] protected int MinLifeTime = 2;
    [SerializeField] protected int MaxLifeTime = 5;

    protected float LifeDuration;
    protected Coroutine DecreaseTimeOfBackPool; 

    public event Action<SpawnObject> Stored;

    private void OnDisable()
    {
        StopCoroutine(DecreaseTimeOfBackPool);
    }

    virtual public void Init(Vector3 position) 
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Renderer.material.color = new Color(0, 0, 0, 1);
        LifeDuration = Random.Range(MinLifeTime, MaxLifeTime);
    }

    protected void StartLife() 
    {
        DecreaseTimeOfBackPool = StartCoroutine(DecreasingTimeOfBackPool());
    }

    virtual protected IEnumerator DecreasingTimeOfBackPool() 
    {
        float currentLifeDuration = 0;

        while (currentLifeDuration < LifeDuration)
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

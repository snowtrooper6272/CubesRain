using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    
    private Coroutine _decreaseTimeOfBackPool;
    private int _lifeDuration;
    private int _minLifeDuration = 2;
    private int _maxLifeDuration = 5;
    private bool _isCollison;

    public event Action<Cube> Stored;

    private void OnDisable()
    {
        if(_decreaseTimeOfBackPool != null)
            StopCoroutine(_decreaseTimeOfBackPool);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCollison == false)
        {
            if (collision.gameObject.TryGetComponent(out Platform platform))
            {
                _renderer.material.color = GenerateRandomColor();
                _lifeDuration = UnityEngine.Random.Range(_minLifeDuration, _maxLifeDuration);
                _decreaseTimeOfBackPool = StartCoroutine(DecreaseTimeOfBackPool());
                _isCollison = true;
            }
        }
    }

    private IEnumerator DecreaseTimeOfBackPool() 
    {
        float currentLifeDuration = 0;

        while (currentLifeDuration < _lifeDuration) 
        {
            currentLifeDuration += Time.deltaTime;
            
            yield return null;
        }

        Stored.Invoke(this);
    }

    public void Init(Vector3 position) 
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        _renderer.material.color = new Color(0,0,0);
        _isCollison = false;
    }

    private Color GenerateRandomColor() 
    {
        return new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
    }
}
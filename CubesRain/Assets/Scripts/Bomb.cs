using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private LayerMask explodingLayer;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Renderer _renderer;

    private Coroutine _exploding;
    private int _minExplosionTime = 2;
    private int _maxExplosionTime = 5;

    public event Action<Bomb> Stored;

    private void OnDisable()
    {
        if(_exploding != null)
            StopCoroutine(_exploding);
    }

    public void Init(Vector3 newPosition) 
    {
        transform.position = newPosition;
        _rigidbody.velocity = Vector3.zero;
        _renderer.material.color = Color.black;

        _exploding = StartCoroutine(Exploding());
    }

    private IEnumerator Exploding() 
    {
        float startAlpha = _renderer.material.color.a;
        int targetAlpha = 0;
        int explosionTime = UnityEngine.Random.Range(_minExplosionTime, _maxExplosionTime);
        float currentTime = 0;

        while (currentTime <= explosionTime) 
        {
             currentTime += Time.deltaTime;
            float normalizeTime = currentTime / explosionTime;

            _renderer.material.color = new Color(_renderer.material.color.r, _renderer.material.color.g, _renderer.material.color.b, Mathf.Lerp(startAlpha, targetAlpha, normalizeTime));

            yield return null;
        }

        Explode();
    }

    private void Explode() 
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radius, explodingLayer.value);

        foreach (var hit in hits) 
        {
            if (hit.TryGetComponent(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _radius);
        }

        Stored.Invoke(this);
    }
}

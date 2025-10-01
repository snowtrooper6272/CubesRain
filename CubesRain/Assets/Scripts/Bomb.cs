using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : SpawnObject
{
    [SerializeField] private float _radius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private LayerMask explodingLayer;
    [SerializeField] private Rigidbody _rigidbody;

    private float _startAlpha = 1;
    private float _targetAlpha = 0;

    public override void Init(Vector3 position)
    {
        base.Init(position);

        _startAlpha = Renderer.material.color.a;
        StartLife();
    }

    protected override void CoroutineUpdate(float currentTime)
    {
        float normalizeTime = currentTime / LifeDuration;

        Renderer.material.color = new Color(Renderer.material.color.r, Renderer.material.color.g, Renderer.material.color.b, Mathf.Lerp(_startAlpha, _targetAlpha, normalizeTime));
    }

    protected override void CoroutineEnd()
    {
        base.CoroutineEnd();

        Collider[] hits = Physics.OverlapSphere(transform.position, _radius, explodingLayer.value);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _radius);
        }
    }
}

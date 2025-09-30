using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : SpawnObject
{
    private bool _isCollison;

    private void OnEnable()
    {
        _isCollison = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCollison == false)
        {
            if (collision.gameObject.TryGetComponent(out Platform platform))
            {
                _renderer.material.color = GenerateRandomColor();
                StartLife();

                _isCollison = true;
            }
        }
    }

    private Color GenerateRandomColor()
    {
        return new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
    }
}
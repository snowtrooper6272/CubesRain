using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
                Renderer.material.color = GenerateRandomColor();
                StartLife();

                _isCollison = true;
            }
        }
    }

    private Color GenerateRandomColor()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    
    private Spawner _spawner;
    private bool _isCollison;

    private int _lifeDuration;
    private float _currentLifeDuration;

    private int _minLifeDuration = 2;
    private int _maxLifeDuration = 5;

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCollison == false)
        {
            if (collision.gameObject.TryGetComponent(out Platform platform))
            {
                _renderer.material.color = GenerateRandomColor();
                _isCollison = true;
                _lifeDuration = Random.Range(_minLifeDuration, _maxLifeDuration);
                StartCoroutine(DecreaseTimeOfBackPool());
            }
        }
    }

    private IEnumerator DecreaseTimeOfBackPool() 
    {
        bool IsDestroy = false;

        while (IsDestroy == false) 
        {
            if (_currentLifeDuration >= _lifeDuration) 
            {
                _spawner.PlaceCube(this);
                IsDestroy = true;
            }

            _currentLifeDuration += Time.deltaTime;
            
            yield return null;
        }
    }

    public void Init(Spawner spawner) 
    {
        _spawner = spawner;
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        _renderer.material.color = new Color(0,0,0);
        _isCollison = false;
        _currentLifeDuration = 0;
    }

    private Color GenerateRandomColor() 
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}
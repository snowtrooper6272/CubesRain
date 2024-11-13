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

    private void Awake()
    {
        _spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCollison == false)
        {
            _renderer.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            _isCollison = true;
            _lifeDuration = Random.Range(_minLifeDuration, _maxLifeDuration);
        }
    }

    private void Update()
    {
        if (_isCollison) 
        {
            if (_currentLifeDuration >= _lifeDuration) 
            {
                _spawner.PlaceCube(this);
            }

            _currentLifeDuration += Time.deltaTime;
        }
    }

    public void Init() 
    {
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        Debug.Log(transform.rotation);

        _renderer.material.color = new Color(0,0,0);
        _isCollison = false;
        _currentLifeDuration = 0;
    }
}
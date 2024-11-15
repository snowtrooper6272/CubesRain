using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private GameObject _startPoint;
    [SerializeField] private int _interval;
    [SerializeField] private int _poolCapacity;

    private int _poolMaxSize = 10;
    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (obj) => poolObjectOnGet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }

    private void poolObjectRelease(Cube returnedObject) 
    {
        returnedObject.gameObject.SetActive(false);
    }

    private void poolObjectOnGet(Cube freeObject) 
    {
        freeObject.Init(this);
        freeObject.transform.position = new Vector3(Random.Range(_startPoint.transform.position.x - _startPoint.transform.localScale.x, _startPoint.transform.position.x + _startPoint.transform.localScale.x),
                                                    _startPoint.transform.position.y,
                                                    Random.Range(_startPoint.transform.position.z - _startPoint.transform.localScale.z, _startPoint.transform.position.z + _startPoint.transform.localScale.z));
        freeObject.gameObject.SetActive(true);
    }
            
    private void Start()
    {
        InvokeRepeating(nameof(FalloutCube), 0f, 1);
    }

    private void FalloutCube() 
    {
        _pool.Get();
    }

    public void PlaceCube(Cube cube) 
    {
        _pool.Release(cube);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Cube cube))
            _pool.Release(cube);
    }
}

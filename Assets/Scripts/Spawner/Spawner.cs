using System;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour, IDestroyable<T>
{
    [SerializeField] private T _prefab;

    private ObjectPool<T> _pool;

    private int _spawnCount = 0;
    private int _activeCount = 0;
    private int _startPoolCount = 30;

    public event Action<int, int> CountChanged;

    private void Awake()
    {
        _pool = new ObjectPool<T>(_prefab, transform, _startPoolCount);
    }

    private void OnDestroy()
    {
        _pool.Reset();
    }

    protected virtual void OnDestroyed(T obj)
    {
        obj.Destroyed -= OnDestroyed;
        _pool.PutObject(obj);

        _activeCount--;
    }

    public void SpawnObject(Vector3 target)
    {
        T obj = _pool.GetObject();
        obj.transform.position = target;
        obj.Destroyed += OnDestroyed;

        _activeCount++;
        _spawnCount++;
        CountChanged?.Invoke(_spawnCount, _activeCount);
    }
}

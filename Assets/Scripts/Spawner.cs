using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private Platform _mainPlatform;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _spawnHeight;

    private ObjectPool<Cube> _cubes;
    private ObjectPool<Bomb> _bombs;

    private int _spawnCubeCount = 0;
    private int _spawnBombCount = 0;
    private int _activeCubeCount = 0;
    private int _activeBombCount = 0;

    private Coroutine _coroutine;
    private Vector3 _platformSize;

    public event Action<int, int> CubesCountChanged;
    public event Action<int, int> BombsCountChanged;

    private void Start()
    {
        _cubes = new ObjectPool<Cube>(_cubePrefab, transform, 10);
        _bombs = new ObjectPool<Bomb>(_bombPrefab, transform, 10);

        _coroutine = StartCoroutine(RainOfCube());
        _platformSize = _mainPlatform.GetSize();
    }

    private void OnDestroy()
    {
        _cubes.Reset();
        _bombs.Reset();

        StopCoroutine(_coroutine);
    }

    private void OnCubeDestroyed(Cube cube)
    {
        cube.Destroyed -= OnCubeDestroyed;
        _cubes.PutObject(cube);

        _activeCubeCount--;

        SpawnBomb(cube.transform);
    }

    private void OnBombDestroyed(Bomb bomb)
    {
        bomb.Destroyed -= OnBombDestroyed;
        _bombs.PutObject(bomb);

        _activeBombCount--;
    }

    private IEnumerator RainOfCube()
    {
        WaitForSeconds delay = new WaitForSeconds(_spawnInterval);

        while (true)
        {
            SpawnCube();

            yield return delay;
        }
    }

    private void SpawnCube()
    {
        float randomX = Random.Range(-_platformSize.x / 2, _platformSize.x / 2);
        float randomZ = Random.Range(-_platformSize.z / 2, _platformSize.z / 2);

        Vector3 spawnPosition = _mainPlatform.transform.position + new Vector3(randomX, _spawnHeight, randomZ);

        Cube cube = _cubes.GetObject();
        cube.transform.position = spawnPosition;
        cube.Destroyed += OnCubeDestroyed;

        _activeCubeCount++;
        _spawnCubeCount++;
        CubesCountChanged?.Invoke(_spawnCubeCount, _activeCubeCount);
    }

    private void SpawnBomb(Transform target)
    {
        Bomb bomb = _bombs.GetObject();
        bomb.transform.position = target.position;
        bomb.Destroyed += OnBombDestroyed;

        _activeBombCount++;
        _spawnBombCount++;
        BombsCountChanged?.Invoke(_spawnBombCount, _activeBombCount);

    }
}

using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private Platform _mainPlatform;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _spawnHeight;
    [SerializeField] private BombSpawner _bombSpawner;

    private Coroutine _coroutine;
    private Vector3 _platformSize;

    private void Start()
    {
        _coroutine = StartCoroutine(RainOfCube());
        _platformSize = _mainPlatform.GetSize();
    }

    private void OnDestroy()
    {
        StopCoroutine(_coroutine);
    }

    protected override void OnDestroyed(Cube cube)
    {
        base.OnDestroyed(cube);

        _bombSpawner.SpawnObject(cube.transform.position);
    }

    private IEnumerator RainOfCube()
    {
        WaitForSeconds delay = new WaitForSeconds(_spawnInterval);

        while (true)
        {
            SpawnObject(GetRandomPoint());

            yield return delay;
        }
    }

    private Vector3 GetRandomPoint()
    {
        float randomX = Random.Range(-_platformSize.x / 2, _platformSize.x / 2);
        float randomZ = Random.Range(-_platformSize.z / 2, _platformSize.z / 2);

        Vector3 spawnPosition = _mainPlatform.transform.position + new Vector3(randomX, _spawnHeight, randomZ);

        return spawnPosition;
    }
}

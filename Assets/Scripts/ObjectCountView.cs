using UnityEngine;
using UnityEngine.UI;

public class ObjectCountView : MonoBehaviour
{
    [SerializeField] private Text _cubesInfo;
    [SerializeField] private Text _bombsInfo;
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private BombSpawner _bombSpawner;

    private void OnEnable()
    {
        _cubeSpawner.CountChanged += OnCubesCountChanged;
        _bombSpawner.CountChanged += OnBombsCountChanged;
    }

    private void OnDisable()
    {
        _cubeSpawner.CountChanged -= OnCubesCountChanged;
        _bombSpawner.CountChanged -= OnBombsCountChanged;
    }

    private void OnCubesCountChanged(int countCubes, int activeCubes)
    {
        _cubesInfo.text = $"{countCubes} / {activeCubes}";
    }

    private void OnBombsCountChanged(int countBombs, int activeBombs)
    {
        _bombsInfo.text = $"{countBombs} / {activeBombs}";
    }
}


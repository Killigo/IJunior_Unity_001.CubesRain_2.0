using UnityEngine;
using UnityEngine.UI;

public class ObjectCountView : MonoBehaviour
{
    [SerializeField] private Text _cubesInfo;
    [SerializeField] private Text _bombsInfo;
    [SerializeField] private Spawner _spawner;

    private void OnEnable()
    {
        _spawner.CubesCountChanged += OnCubesCountChanged;
        _spawner.BombsCountChanged += OnBombsCountChanged;
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }

    private void OnDisable()
    {
        _spawner.CubesCountChanged -= OnCubesCountChanged;
        _spawner.BombsCountChanged -= OnBombsCountChanged;
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


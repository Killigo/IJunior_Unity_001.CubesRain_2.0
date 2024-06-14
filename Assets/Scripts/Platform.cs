using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class Platform : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public Vector3 GetSize()
    {
        return _renderer.bounds.size;
    }
}

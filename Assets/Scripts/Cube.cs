using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour, IDestroyable<Cube>
{
    [SerializeField] private Color _default—olor;
    [SerializeField] private Color _activeColor;

    private Renderer _renderer;
    private int _minLifetime = 2;
    private int _maxLifetime = 5;
    private bool _isContact;

    public event Action<Cube> Destroyed;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        _isContact = false;
        _renderer.material.color = _default—olor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isContact && collision.gameObject.TryGetComponent<Platform>(out Platform component))
        {
            _isContact = true;
            _renderer.material.color = _activeColor;
            StartCoroutine(Deactivate());
        }
    }

    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(Random.Range(_minLifetime, _maxLifetime));
        Destroyed?.Invoke(this);
    }
}

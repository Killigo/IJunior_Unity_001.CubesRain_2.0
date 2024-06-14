using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]

public class Bomb : MonoBehaviour, IDestroyable<Bomb>
{
    private float _minFadeTime = 2f;
    private float _maxFadeTime = 5f;
    private float _fadeTime;
    private float _explosionRadius = 30f;
    private float _explosionForce = 500f;
    private Renderer _renderer;

    public event Action<Bomb> Destroyed;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        _fadeTime = Random.Range(_minFadeTime, _maxFadeTime);

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float timer = 0;
        Color initialColor = _renderer.material.color;

        while (timer < _fadeTime)
        {
            float alpha = 1 - (timer / _fadeTime);
            _renderer.material.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            timer += Time.deltaTime;

            yield return null;
        }

        Explode();
        Destroyed?.Invoke(this);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider collider in colliders)
            if (collider.attachedRigidbody != null)
                collider.attachedRigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }
}

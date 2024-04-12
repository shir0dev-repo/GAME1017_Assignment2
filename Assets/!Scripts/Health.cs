using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 3;

    public Action<int> OnDamageTaken;
    public Action OnDeath;
    public Action<bool> OnInvulnerabilityChanged;

    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; private set; }
    public bool IsInvulnerable { get; private set; } = false;

    private float _invulnerabilityTimer = 0;
    private Coroutine _invulnCO = null;
    private void Awake() => MaxHealth = CurrentHealth = _maxHealth;

    public void TakeDamage(int damage)
    {
        if (IsInvulnerable) return;

        CurrentHealth -= damage;
        OnDamageTaken?.Invoke(CurrentHealth);

        if (CurrentHealth <= 0)
            Die();
    }

    private void Die()
    {
        CurrentHealth = 0;
        OnDeath?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            TakeDamage(1);
    }

    public void SetInvulerable(float durationInSeconds)
    {
        _invulnerabilityTimer += durationInSeconds;
        if (_invulnCO == null)
            StartCoroutine(InvulnCoroutine());
    }

    private IEnumerator InvulnCoroutine()
    {
        IsInvulnerable = true;
        OnInvulnerabilityChanged?.Invoke(true);
        while (_invulnerabilityTimer > 0)
        {
            _invulnerabilityTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        _invulnerabilityTimer = 0;
        IsInvulnerable = false;
        OnInvulnerabilityChanged?.Invoke(false);
    }
}

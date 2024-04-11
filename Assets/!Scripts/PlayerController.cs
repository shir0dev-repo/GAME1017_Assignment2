using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
[SelectionBase]
public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private float _moveSpeed = 8f, _jumpForce = 4f, _slideForce = 12f;
    [SerializeField] private Collider2D _baseCollider, _slideCollider;
    [SerializeField] private Health _health;

    public Health PlayerHealth => _health;

    private Rigidbody2D _rb2d;
    private Animator _anim;

    private bool _grounded, _sliding;
    private float _inputDirection = 0;

    protected override void Awake()
    {
        base.Awake();

        _rb2d = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
        _health = GetComponent<Health>();
        _health.OnDamageTaken += OnDamageTaken;
        _health.OnDeath += OnDeath;
    }

    private void Update()
    {
        _inputDirection = Input.GetAxisRaw("Horizontal");

        if (_grounded && Input.GetKeyDown(KeyCode.Space))
            DoJump();
        if (_grounded && Input.GetKeyDown(KeyCode.S))
            DoSlide();
    }

    private void FixedUpdate()
    {
        _grounded = IsGrounded();
        if (_sliding) return;
        _anim.SetBool("_isGrounded", _grounded);
        float verticalVelocity = _rb2d.velocity.y;
        Vector3 forceDirection = new Vector3(_inputDirection * _moveSpeed, verticalVelocity, 0);
        _rb2d.velocity = forceDirection;
    }

    private void DoJump()
    {
        _rb2d.AddForce(Vector3.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void DoSlide()
    {
        if (_sliding) return;
        _baseCollider.enabled = false;
        _slideCollider.enabled = true;
        _sliding = true;
        _rb2d.velocity = Vector3.right * _slideForce;
        _anim.SetBool("_sliding", _sliding);
        Invoke(nameof(FinishSlide), 0.5f);
    }
    private void FinishSlide()
    {
        _sliding = false;
        _baseCollider.enabled = true;
        _slideCollider.enabled = false;
        _anim.SetBool("_sliding", _sliding);
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector3.down, 1f, 1 << LayerMask.NameToLayer("Ground"));
    }

    private void OnDamageTaken(int obj)
    {
        _anim.SetTrigger("_damageTaken");
        StartCoroutine(DamageDisplayCoroutine(10f));
    }
    private void OnDeath()
    {
        _anim.SetTrigger("_onDeath");
    }

    private IEnumerator DamageDisplayCoroutine(float duration)
    {
        _health.SetInvulerable(duration);
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();

        sr.material.color = new(1, 1, 1, 0.4f);

        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        sr.material.color = Color.white;
    }
}

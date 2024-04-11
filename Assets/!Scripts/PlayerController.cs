using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
[SelectionBase]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 8f, _jumpForce = 4f;
    private Rigidbody2D _rb2d;
    public bool grounded;
    private float _inputDirection = 0;
    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        _inputDirection = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
            DoJump();
    }

    private void DoJump()
    {
        StartCoroutine(AddForceOverTime(Vector3.up * _jumpForce));
        //_rb2d.AddForce(Vector3.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        grounded = IsGrounded();
        float verticalVelocity = _rb2d.velocity.y;
        Vector3 forceDirection = new Vector3(_inputDirection * _moveSpeed, verticalVelocity, 0);
        _rb2d.velocity = forceDirection;
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector3.down, 1f, 1 << LayerMask.NameToLayer("Ground"));
    }


    private IEnumerator AddForceOverTime(Vector3 force)
    {
        _rb2d.AddForce(force, ForceMode2D.Impulse);
        Debug.Log("Waiting!");
        yield return new WaitUntil(() => _rb2d.velocity.y <= 0f);
        Debug.Log("finished waiting!");

        while (!grounded)
        {
            Debug.Log(grounded);
            _rb2d.AddForce(-force * 0.5f, ForceMode2D.Impulse);
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + Vector3.down * 0.5f, 0.5f);
    }
}

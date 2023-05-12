using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Animator animator;
    private Vector2 _inputMovement;
    private bool _canMove = true;
    private static readonly int X = Animator.StringToHash("X");
    private static readonly int Y = Animator.StringToHash("Y");

    private void Start()
    {
        InputManager.Instance.onMovementInputChanged += InputMovementChanged;
    }

    private void FixedUpdate()
    {
        if (!_canMove) return;
        switch (_inputMovement.x)
        {
            case > 0:
                animator.SetFloat(X, 1);
                break;
            case < 0:
                animator.SetFloat(X, -1);
                break;
            default:
                animator.SetFloat(X, 0);
                break;
        }
        switch (_inputMovement.y)
        {
            case > 0:
                animator.SetFloat(Y, 1);
                break;
            case < 0:
                animator.SetFloat(Y, -1);
                break;
            default:
                animator.SetFloat(Y, 0);
                break;
        }

        rb.MovePosition(rb.position + speed * _inputMovement * Time.fixedDeltaTime);
    }

    private void InputMovementChanged(Vector2 value)
    {
        _inputMovement = value;
    }

    public void StartMovement()
    {
        _canMove = true;
    }
    public void StopMovement()
    {
        _canMove = false;
        _inputMovement = Vector2.zero;
    }
}

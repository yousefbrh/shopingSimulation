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
    private static readonly int X = Animator.StringToHash("X");
    private static readonly int Y = Animator.StringToHash("Y");

    private void Start()
    {
        InputManager.Instance.onMovementInputChanged += InputMovementChanged;
    }

    private void FixedUpdate()
    {
        _inputMovement.x = Input.GetAxis("Horizontal");
        _inputMovement.y = Input.GetAxis("Vertical");
        animator.SetFloat(X, _inputMovement.x);
        animator.SetFloat(Y, _inputMovement.y);

        rb.MovePosition(rb.position + speed * _inputMovement * Time.fixedDeltaTime);
    }

    private void InputMovementChanged(Vector2 value)
    {
        _inputMovement = value;
    }
}

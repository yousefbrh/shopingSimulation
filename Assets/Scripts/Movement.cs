using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    private Vector2 _inputMovement;

    private void FixedUpdate()
    {
        _inputMovement.x = Input.GetAxis("Horizontal");
        _inputMovement.y = Input.GetAxis("Vertical");
        
        rb.MovePosition(rb.position + speed * _inputMovement * Time.fixedDeltaTime);
    }
}

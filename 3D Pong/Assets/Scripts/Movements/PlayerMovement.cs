using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed; // movespeed of player

    public Vector3 playerMoveDir { get; private set; }
    float _x;
    float _y;
    
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        DoMovement();
    }

    /// <summary>
    /// Does movement of player based on WASD key input and movespeed
    /// </summary>
    private void DoMovement()
    {
        _x = Input.GetAxisRaw("Horizontal");
        _y = Input.GetAxisRaw("Vertical");
        playerMoveDir = new Vector3(_x, _y, 0f);
        rb.velocity = playerMoveDir * moveSpeed ;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBallMovement : MonoBehaviour
{
    [SerializeField] private float ballLaunchAngle; // launches the ball using random angle within this range
    [SerializeField] private float moveSpeed;
    private float actualMoveSpeed => Mathf.Clamp(moveSpeed, 10f, 100f);

    GameObject playerGO; // player GameObject
    Rigidbody rb; // ball's rigidbody
    Vector3 lastVelocity;
    Vector3 ballMoveDir; // ball launched direction

    private void Awake()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        InitiateBallLaunch();
    }


    private void Update()
    {
        lastVelocity = rb.velocity;
        //rb.velocity = ballMoveDir * actualMoveSpeed;
    }
    /*
     * Initiates the ball on the other direction of player
     * Randomly generates the 
     */
    private void InitiateBallLaunch()
    {
        ballMoveDir = GetRandomForwardDirection(ballLaunchAngle, transform.forward);
        rb.velocity = ballMoveDir * actualMoveSpeed;   
    }

    /// <summary>
    /// callback event ran when ability starts
    /// </summary>
    /// <param name="angle">range of angle that faces direction</param>
    /// <param name="forward">forward direction of the object</param>
    /// /// <return>the forward random direction within the given angle</return>
    public static Vector3 GetRandomForwardDirection(float angle, Vector3 forward)
    {
        float _angle = Random.Range(angle * -1, angle); // angle between -angle < x < angle
        // Gets the random direction of the ball from the angle
        Vector3 moveDir = Quaternion.AngleAxis(_angle, new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f)) * forward;
        return moveDir.normalized;
    }

    // change initialDir to the norma
    private void OnCollisionEnter(Collision collision)
    {
        ballMoveDir = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        rb.velocity = ballMoveDir * Mathf.Max(actualMoveSpeed, 0f);
    }
}

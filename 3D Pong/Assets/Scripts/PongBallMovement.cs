using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Class for Pong ball Physics Movement
/// </summary>
public class PongBallMovement : MonoBehaviour
{
    /// <summary>
    /// How much degrees the ball launch at the start of the game
    /// </summary>
    [SerializeField] 
    private float ballLaunchAngle;
    
    /// <summary>
    /// Ball's moveSpeed
    /// </summary>
    [SerializeField] 
    private float moveSpeed;
    
    /// <summary>
    /// Speed used to move the ball 
    /// </summary>
    private float actualMoveSpeed => Mathf.Clamp(moveSpeed, 10f, 100f); 
    private float originSpeed; // start speed of the ball

    GameObject playerGO; // player GameObject
    Rigidbody rb; // ball's rigidbody
    Vector3 lastVelocity; // last velocity of the ball
    Vector3 ballMoveDir; // ball launched direction

    public UnityAction<Collision> OnBallCollisionEnter; // ball collision enter event
    public UnityAction<Collision> OnBallCollisionExit; // ball collision exit event

    private void Awake()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        originSpeed = actualMoveSpeed;
    }

    private void OnEnable()
    {
        GameController.Instance.OnGameRestart += ResetBallSpeed;    
    }

    private void OnDisable()
    {
        GameController.Instance.OnGameRestart -= ResetBallSpeed;
    }

    // Constantly updates the velocity of the ball
    private void Update()
    {
        lastVelocity = rb.velocity;
    }

    /// <summary>
    /// Throws the ball to the end of the ball in random direction
    /// </summary>
    private void InitiateBallLaunch()
    {
        ballMoveDir = GetRandomForwardDirection(ballLaunchAngle, transform.forward);
        rb.velocity = ballMoveDir * actualMoveSpeed;   
    }

    /// <summary>
    /// Gets the random forward direction from the given forward direction
    /// </summary>
    /// <param name="angle">range of angle that faces direction</param>
    /// <param name="forward">forward direction of the object</param>
    /// <return>the forward random direction within the given angle</return>
    public static Vector3 GetRandomForwardDirection(float angle, Vector3 forward)
    {
        // angle between -angle < x < angle
        float _angle = Random.Range(angle * -1, angle);

        // axis to shoot the ball, X and Y axis 
        Vector3 axis = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
        
        // Gets the random direction of the ball from the angle
        Vector3 moveDir = Quaternion.AngleAxis(_angle, axis) * forward;

        // normalize the vector to get constant speed 
        return moveDir.normalized; 
    }

    /// <summary>
    /// Increases the ball speed
    /// <param name="increaseValue">Speed value to be increased</param>
    /// </summary>
    public void IncrementSpeed(float increaseValue)
    {
        moveSpeed += increaseValue;
    }

    // Resets the ball speed to original speed
    private void ResetBallSpeed()
    {
        moveSpeed = originSpeed;
    }

    /// <summary>
    /// change the direction of the ball based on the direction of the ball
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        ballMoveDir = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        rb.velocity = ballMoveDir * Mathf.Max(actualMoveSpeed, 0f);
        OnBallCollisionEnter?.Invoke(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        OnBallCollisionExit?.Invoke(collision);
    }
}

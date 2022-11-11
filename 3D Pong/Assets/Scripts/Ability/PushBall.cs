using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Litkey/Ability/PushBall")]
public class PushBall : Ability
{
    [Header("PushBall Settings")]
    [SerializeField] private float pushForce;
    [SerializeField] private float angle = 30f;
    GameObject ball;

    public override void OnAbilityStart(GameObject parent)
    {
        base.OnAbilityStart(parent);
        ball = GameObject.FindGameObjectWithTag("Ball");
        Rigidbody ballRB = ball.GetComponent<Rigidbody>();
        ballRB.isKinematic = false;
        ballRB.AddForce(PongBallMovement.GetRandomForwardDirection(angle, ball.transform.forward) * pushForce, ForceMode.Impulse);
    }
}

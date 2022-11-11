using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    GameObject player;

    AbilityHolder restartHolder; // ability holder of restart
    AbilityHolder pushBallHolder; // ability holder of pushBall

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        restartHolder = AbilityHolder.GetAbilityHolderOfType(player, eAbilityType.Restart);
        pushBallHolder = AbilityHolder.GetAbilityHolderOfType(player, eAbilityType.PushBall);
    }

    private void OnEnable()
    {
        GameController.Instance.OnGameRestart += OnRestart;
        GameController.Instance.OnGameEnd += OnEnd;
    }

    private void OnDisable()
    {
        GameController.Instance.OnGameRestart -= OnRestart;
        GameController.Instance.OnGameEnd -= OnEnd;
    }

    private void OnRestart()
    {
        restartHolder.isLocked = true;
        pushBallHolder.isLocked = false;
    }

    private void OnEnd()
    {
        restartHolder.isLocked = false;
        pushBallHolder.isLocked = true;
    }
}

using UnityEngine;

public enum eAbilityType
{
    PushBall,
    Restart
}

[CreateAssetMenu(menuName = "Litkey/Ability/base")]
public class Ability : ScriptableObject
{
    [Header("Debug")]
    public bool showLog;

    [Header("Main Ability Settings")]
    public new string name; // ability name
    public eAbilityType _abilityType;
    public float _coolDownTime; // cooldown time for ability
    public float _activeTime; // time to start cooldown after ability is activated, normally runs until the player's on key up event
    [TextArea]
    public string _description; // description of the ability
    public KeyCode _key; // ability use key 
    public bool _Instantaneous; // will allow to run OnAbilityRunning even when player on key up

    [SerializeField]
    protected bool _isUsingAbility; // when player is holding on a key(charge?), 

    /// <summary>
    /// callback event ran when ability starts
    /// </summary>
    /// <param name="parent">the gameObject Ability Holder is attached to</param>
    public virtual void OnAbilityStart(GameObject parent)
    {
        if (showLog)
        {
            Debug.Log(name + " Ability Started");
        }
        _isUsingAbility = true;
    }

    /// <summary>
    /// callback event ran when ability ended
    /// </summary>
    /// <param name="parent">the gameObject Ability Holder is attached to</param>
    public virtual void OnAbilityEnd(GameObject parent)
    {
        if (showLog)
        {
            Debug.Log(name + " Ability Ended");
        }
        _isUsingAbility = false;
    }

    /// <summary>
    /// callback event ran when ability is running (player is holding a key)
    /// </summary>
    /// <param name="parent">the gameObject Ability Holder is attached to</param>
    public virtual void OnAbilityRunning(GameObject parent)
    {
        if (showLog)
        {
            Debug.Log(name + " Ability Running");
        }
    }
}

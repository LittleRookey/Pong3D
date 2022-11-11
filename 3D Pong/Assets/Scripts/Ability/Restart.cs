using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Litkey/Ability/RestartGame")]
public class Restart : Ability
{
    AbilityHolder abHolder;

    // Restarts the Game
    public override void OnAbilityStart(GameObject parent)
    {
        base.OnAbilityStart(parent);
        GameController.Instance.Restart();

        
    }

    // Locks the Restart Ability
    public override void OnAbilityEnd(GameObject parent)
    {
        base.OnAbilityEnd(parent);
        

        abHolder = AbilityHolder.GetAbilityHolderOfType(parent, _abilityType);
        abHolder.isLocked = true;

    }
    
}

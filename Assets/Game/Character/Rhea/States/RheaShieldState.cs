using System;
using UnityEngine;

public class RheaShieldState : RheaState
{
    private GameObject shield;

    override public void Enter(RheaStateInput input, CharacterStateTransitionInfo transitionInfo = null)
    {
        input.slowSprite.enabled = false;
        shield = UnityEngine.Object.Instantiate(input.shieldPrefab, character.transform);
    }

    override public void Update(RheaStateInput input)
    {
        input.slowSprite.enabled = false;
        if (SpellMap.Instance.GetSpellDown(input.cc.playerNumber, SpellType.PRIM))
        {
            MatchManager.Instance.StartLastWord(input.cc.playerNumber);
        }
        if (shield == null)
        {
            if (character.softTransitionChangeState != null)
                character.softTransitionChangeState();
            else
                character.ChangeState<RheaIdleState>();
        }

        HandleMoveAnimation(input);
    }

    override public void SoftTransitionWarning(RheaStateInput input)
    {
        GameObject.Destroy(shield);
    }

    override public void FixedUpdate(RheaStateInput input)
    {
        input.cc.HandleMovement();
    }
}

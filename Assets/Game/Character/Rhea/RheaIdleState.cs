using System;
using UnityEngine;

public class RheaIdleState : RheaState
{
    private int prevHorizontalMovement;

    override public void Enter(RheaStateInput input, CharacterStateTransitionInfo info = null)
    {
        prevHorizontalMovement = 0;
        input.anim.Play("Idle");
    }

    override public void Update(RheaStateInput input)
    {
        input.cc.HandlePlaceCircle();

        // Spells
        if (SpellMap.Instance.GetSpellDown(input.cc.playerNumber, SpellType.INTRIN))
        {
            character.ChangeState<RheaShieldState>();
        }
        // TODO: This triggers the last word. Yun can remove this to cast the primary spell.
        if (SpellMap.Instance.GetSpellDown(input.cc.playerNumber, SpellType.PRIM))
        {
            MatchManager.Instance.StartLastWord(input.cc.playerNumber);
        }

        if (SpellMap.Instance.GetSpellDown(input.cc.playerNumber, SpellType.MOVE))
        {
            character.ChangeState<RheaDashState>();
        }

        // Movement animations
        int horizontalMovement = 0;
        if (InputMap.Instance.GetInput(input.cc.playerNumber, ActionType.RIGHT))
        {
            horizontalMovement++;
        }
        if (InputMap.Instance.GetInput(input.cc.playerNumber, ActionType.LEFT))
        {
            horizontalMovement--;
        }
        if (prevHorizontalMovement != horizontalMovement)
        {
            if (horizontalMovement != 0)
            {
                input.anim.Play("StartMove");
                input.sr.flipX = horizontalMovement != 1;
            }
            else
            {
                input.anim.Play("StopMove");
            }
        }
        prevHorizontalMovement = horizontalMovement;
    }

    override public void FixedUpdate(RheaStateInput input)
    {
        input.cc.HandleMovement();
    }
}
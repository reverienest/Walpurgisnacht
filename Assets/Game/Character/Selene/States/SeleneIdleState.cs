using System;
using UnityEngine;

public class SeleneIdleState : SeleneState
{
    private int prevHorizontalMovement;

    override public void Enter(SeleneStateInput input, CharacterStateTransitionInfo info = null)
    {
        prevHorizontalMovement = 0;
        input.anim.Play("Idle");
    }

    override public void Update(SeleneStateInput input)
    {
        input.cc.HandlePlaceCircle();

        input.cc.HandleLastWords();

        // Spells
        if (SpellMap.Instance.GetSpellDown(input.cc.playerNumber, SpellType.INTRIN))
        {
            character.ChangeState<SeleneTeleportState>();
        }
        // TODO: This triggers the last word. Yun can remove this to cast the primary spell.
        if (SpellMap.Instance.GetSpellDown(input.cc.playerNumber, SpellType.PRIM))
        {
            
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
                input.sr.flipX = horizontalMovement == 1;
            }
            else
            {
                input.anim.Play("StopMove");
            }
        }
        prevHorizontalMovement = horizontalMovement;
    }

    override public void FixedUpdate(SeleneStateInput input)
    {
        input.cc.HandleMovement();
    }
}
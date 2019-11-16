using System;
using UnityEngine;

public class RheaShieldState : RheaState
{
    private enum Sequence { START_SHIELD, HOLD_SHIELD, END_SHIELD }
    private Sequence state;
    private GameObject shield;
    private int prevHorizontalMovement;


    override public void Enter(RheaStateInput input, CharacterStateTransitionInfo transitionInfo = null)
    {
        state = Sequence.START_SHIELD;
        //input.anim.Play("StartShield");
        shield = UnityEngine.Object.Instantiate(input.shield, character.transform);
    }

    override public void Update(RheaStateInput input)
    {
        if (shield == null)
        {
            state = Sequence.END_SHIELD;
            character.ChangeState<RheaIdleState>();
            input.anim.Play("EndShield");
        }
        

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

    override public void SoftTransitionWarning(RheaStateInput input)
    {
        state = Sequence.END_SHIELD;
        input.anim.Play("ExitHole");
    }

    override public void OnAnimationEvent(string eventName)
    {
        if (eventName == "ShieldStarted" && state == Sequence.START_SHIELD)
        {
            state = Sequence.HOLD_SHIELD;
        }
        else if (eventName == "ShieldEnded" && state == Sequence.END_SHIELD)
        {
            if (character.softTransitionChangeState != null)
                character.softTransitionChangeState();
            else
                character.ChangeState<RheaIdleState>();
        }
    }

    override public void FixedUpdate(RheaStateInput input)
    {
        input.cc.HandleMovement();
    }
}

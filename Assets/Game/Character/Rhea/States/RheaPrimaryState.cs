using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RheaPrimaryState : RheaState
{
    private Trigger shoot;

    override public void Enter(RheaStateInput input, CharacterStateTransitionInfo transitionInfo = null)
    {
        input.anim.Play("Cast");
    }

    override public void Update(RheaStateInput input)
    {
        if (shoot.Value)
            input.shot.SPSpell();;
    }

    override public void OnAnimationEvent(string eventName)
    {
        if (eventName == "SwordReady")
        {
            shoot.Value = true;
        }
        else if (eventName == "CastEnd")
        {
            character.ChangeState<RheaIdleState>();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RheaHeavyState : RheaState
{
    private Trigger shoot;

    override public void Enter(RheaStateInput input, CharacterStateTransitionInfo transitionInfo = null)
    {
        input.anim.Play("Cast");
        AudioManager.instance.Play("RheaSpellCasting2");
    }

    override public void Update(RheaStateInput input)
    {
        if (shoot.Value)
        {
            input.shot.RheaHeavy(input.shot.startShot);
            AudioManager.instance.Play("RheaLastWordSequence");
        }
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
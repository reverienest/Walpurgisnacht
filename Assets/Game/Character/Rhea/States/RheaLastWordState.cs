using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RheaLastWordState : RheaState
{
    override public void Enter(RheaStateInput input, CharacterStateTransitionInfo info = null)
    {
        input.anim.Play("Idle");
        input.ch.vulnerable = false;
        input.lw.enabled = true;
    }

    override public void Update(RheaStateInput input)
    {

    }

    override public void SoftTransitionWarning(RheaStateInput input)
    {
        input.ch.vulnerable = true;
        input.lw.enabled = false;
        character.softTransitionChangeState(); // Go willingly into that good night
    }

    override public void FixedUpdate(RheaStateInput input)
    {
        // input.cc.HandleMovement();
    }
}

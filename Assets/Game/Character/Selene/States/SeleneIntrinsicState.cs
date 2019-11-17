using UnityEngine;

public class SeleneIntrinsicState : SeleneState
{
    private enum Sequence { INITIAL, END }
    private Sequence state;

    public SeleneFire player;

    override public void Enter(SeleneStateInput input, CharacterStateTransitionInfo transitionInfo = null)
    {
        state = Sequence.INITIAL;
        input.anim.Play("SeleneCast");
        input.shot.SISpell();
    }

    override public void OnAnimationEvent(string eventName)
    {
        if (eventName == "CastStart" && state == Sequence.INITIAL)
        {
            state = Sequence.END;
        }
        else if (eventName == "CastEnd" || state == Sequence.END)
        {
            character.ChangeState<SeleneIdleState>();
        }
    }
}


using UnityEngine;

public class SeleneIntrinsicState : SeleneState
{
    private enum Sequence { INITIAL, END }
    private Sequence state;

    public SeleneFire player;

    override public void Enter(SeleneStateInput input, CharacterStateTransitionInfo transitionInfo = null)
    {
        state = Sequence.INITIAL;
        //input.anim.Play("SeleneCast");
    }

    override public void OnAnimationEvent(string eventName)
    {
        if (eventName == "CastStart" && state == Sequence.INITIAL)
        {
            player.SISpell();
            state = Sequence.END;
        }
        else if (eventName == "CastEnd" || state == Sequence.END)
        {
            if (character.softTransitionChangeState != null)
                character.softTransitionChangeState();
            else
                character.ChangeState<SeleneIdleState>();
        }
    }
}


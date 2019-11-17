using UnityEngine;

public class SelenePrimaryState : SeleneState
{
    private enum Sequence { INITIAL, END }
    private Sequence state;

    //TODO: Implement animation events and create animations
    override public void Enter(SeleneStateInput input, CharacterStateTransitionInfo transitionInfo = null)
    {
        state = Sequence.INITIAL;
        input.anim.Play("SeleneCast");
        input.shot.SPSpell();
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

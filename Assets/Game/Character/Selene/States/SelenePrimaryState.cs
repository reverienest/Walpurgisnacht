using UnityEngine;

public class SelenePrimaryState : SeleneState
{
    private Trigger shoot;

    override public void Enter(SeleneStateInput input, CharacterStateTransitionInfo transitionInfo = null)
    {
        input.anim.Play("Cast");
    }

    override public void Update(SeleneStateInput input)
    {
        if (shoot.Value)
            input.shot.SPSpell();
    }

    override public void OnAnimationEvent(string eventName)
    {
        if (eventName == "HolesReady")
        {
            shoot.Value = true;
        }
        else if (eventName == "CastEnd")
        {
            character.ChangeState<SeleneIdleState>();
        }
    }
}

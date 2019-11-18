using UnityEngine;

public class RheaForceMoveState : RheaState
{
    private ForceMoveTransitionInfo transitionInfo;

    private Vector2 travelOrigin;
    private float travelTimer;

    override public void Enter(RheaStateInput input, CharacterStateTransitionInfo transitionInfo = null)
    {
        this.transitionInfo = (ForceMoveTransitionInfo)transitionInfo;
        travelOrigin = character.transform.position;
        travelTimer = this.transitionInfo.moveTime;
    }

    override public void Update(RheaStateInput input)
    {
        travelTimer -= Time.deltaTime;
        character.transform.position = Vector2.Lerp(transitionInfo.moveDestination, travelOrigin, travelTimer / transitionInfo.moveTime);

        if (travelTimer <= 0)
        {
            transitionInfo.onCompleteMove(() =>
            {
                if (transitionInfo.isLastWordCaster)
                    character.ChangeState<RheaLastWordState>();
                else
                    character.ChangeState<RheaIdleState>();
            });
        }
    }
}

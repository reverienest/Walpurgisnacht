using UnityEngine;

public class SeleneForceMoveState : SeleneState
{
    private ForceMoveTransitionInfo transitionInfo;

    private Vector2 travelOrigin;
    private float travelTimer;

    override public void Enter(SeleneStateInput input, CharacterStateTransitionInfo transitionInfo = null)
    {
        travelOrigin = character.transform.position;
        this.transitionInfo = (ForceMoveTransitionInfo)transitionInfo;
        travelTimer = this.transitionInfo.moveTime;
    }

    override public void Update(SeleneStateInput input)
    {
            travelTimer -= Time.deltaTime;
            character.transform.position = Vector2.Lerp(transitionInfo.moveDestination, travelOrigin, travelTimer / transitionInfo.moveTime);

            if (travelTimer <= 0)
            {
                transitionInfo.onCompleteMove(() =>
                {
                    if (transitionInfo.isLastWordCaster)
                        character.ChangeState<SeleneIdleState>(); //TODO: Change this to SeleneLastWordState when that's implemented
                    else
                        character.ChangeState<SeleneIdleState>();
                });
            }
    }
}

using UnityEngine;

public class SeleneTeleportState : SeleneState
{
    private enum Sequence { ENTER_HOLE, TRAVEL, EXIT_HOLE }
    private Sequence state;

    private Vector2 travelOrigin;
    private Vector2 travelDestination;
    private float travelTime;
    private float travelTimer;

    override public void Enter(SeleneStateInput input, CharacterStateTransitionInfo transitionInfo = null)
    {
        travelOrigin = character.transform.position;

        //Make sure we don't teleport out of the map
        RaycastHit2D hit = Physics2D.Raycast(travelOrigin, input.cc.LastDirection, input.maxTravelDistance, LayerMask.GetMask("Arena"));
        float travelDistance = hit.collider ? (hit.distance - input.cc2d.radius) : input.maxTravelDistance;
        if (InputMap.Instance.GetInput(0, ActionType.RIGHT) || InputMap.Instance.GetInput(0, ActionType.LEFT) || InputMap.Instance.GetInput(0, ActionType.UP) || InputMap.Instance.GetInput(0, ActionType.DOWN))
        {
            travelDestination = travelOrigin + input.cc.LastDirection * travelDistance;
        }
        else
        {
            travelDestination = travelOrigin;

        }
        //travelDestination = travelOrigin + input.cc.LastDirection * travelDistance;

        travelTime = travelDistance / input.maxTravelDistance * input.maxTravelTime;
        travelTimer = travelTime;

        state = Sequence.ENTER_HOLE;
        input.anim.Play("EnterHole");
    }

    override public void Update(SeleneStateInput input)
    {
        if (state == Sequence.TRAVEL)
        {
            travelTimer -= Time.deltaTime;

            if (travelTimer <= 0)
            {
                character.transform.position = travelDestination;
                state = Sequence.EXIT_HOLE;
                input.anim.Play("ExitHole");
            }
        }
    }

    override public void SoftTransitionWarning(SeleneStateInput input)
    {
        state = Sequence.EXIT_HOLE;
        input.anim.Play("ExitHole");
    }

    override public void OnAnimationEvent(string eventName)
    {
        if (eventName == "HoleEntered" && state == Sequence.ENTER_HOLE)
        {
            state = Sequence.TRAVEL;
        }
        else if (eventName == "HoleExited" && state == Sequence.EXIT_HOLE)
        {
            if (character.softTransitionChangeState != null)
                character.softTransitionChangeState();
            else
                character.ChangeState<SeleneIdleState>();
        }
    }
}

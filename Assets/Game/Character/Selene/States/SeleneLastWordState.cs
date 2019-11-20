using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleneLastWordState : SeleneState
{
    private float lastAngleOffset = 0;

    private float largeBulletFiredAt = 0;

    private readonly float secondsPerLastWordBullet = 5f;

    //TODO: Implement animation events and create animations
    override public void Enter(SeleneStateInput input, CharacterStateTransitionInfo transitionInfo = null)
    {
        MatchManager.Instance.OnLastWordEnd += OnLastWordEnd;
    }

    override public void Update(SeleneStateInput input)
    {
        lastAngleOffset += Random.Range(8,12);
        input.shot.SLastWord(lastAngleOffset);

        if (Time.time - largeBulletFiredAt > secondsPerLastWordBullet)
        {
            largeBulletFiredAt = Time.time;
            input.shot.SLastWordLarge();
        }
    }

    public void OnLastWordEnd()
    {
        character.ChangeState<SeleneIdleState>();
    }
}

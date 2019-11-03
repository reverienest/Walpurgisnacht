using UnityEngine;

public class SeleneIdleState : SeleneState
{
    override public void Enter(SeleneStateInput input, CharacterStateTransitionInfo info = null)
    {
        input.anim.Play("Idle");
    }

    override public void Update(SeleneStateInput input)
    {
        input.cc.HandlePlaceCircle();
        if (SpellMap.Instance.GetSpellDown(input.cc.playerNumber, SpellType.INTRIN))
        {
            character.ChangeState<SeleneTeleportState>();
        }
        if (SpellMap.Instance.GetSpellDown(input.cc.playerNumber, SpellType.PRIM))
        {
            MatchManager.Instance.StartLastWord(input.cc.playerNumber);
        }
    }

    override public void FixedUpdate(SeleneStateInput input)
    {
        input.cc.HandleMovement();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RheaLastWordState : RheaState
{
    private float beamTimer;
    private float bulletTimer;

    override public void Enter(RheaStateInput input, CharacterStateTransitionInfo info = null)
    {
        input.anim.Play("Idle");
        beamTimer = input.beamInterval;
        bulletTimer = input.beamInterval;
    }

    override public void Update(RheaStateInput input)
    {
        beamTimer -= Time.deltaTime;
        bulletTimer -= Time.deltaTime;

        if (beamTimer <= 0)
        {
            beamTimer = input.beamInterval;
            Beam beam = GameObject.Instantiate(input.beamPrefab, character.transform.position, Quaternion.identity).GetComponent<Beam>();
            int opposingPlayerNumber = input.cc.playerNumber == 0 ? 1 : 0;
            beam.origin = character.transform;
            beam.target = MatchManager.Instance.Players[opposingPlayerNumber].transform;
        }
        if (bulletTimer <= 0)
        {
            bulletTimer = input.beamInterval;

        }
    }

    override public void FixedUpdate(RheaStateInput input)
    {
        // input.cc.HandleMovement();
    }
}

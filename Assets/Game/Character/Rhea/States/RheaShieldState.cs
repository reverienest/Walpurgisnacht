﻿using System;
using UnityEngine;

public class RheaShieldState : RheaState
{
    private GameObject shield;

    override public void Enter(RheaStateInput input, CharacterStateTransitionInfo transitionInfo = null)
    {
        shield = UnityEngine.Object.Instantiate(input.shieldPrefab, character.transform);
        AudioManager.instance.Play("RheaIntrinsicShield");
    }

    override public void Update(RheaStateInput input)
    {
        if (shield == null)
        {
            if (character.softTransitionChangeState != null)
                character.softTransitionChangeState();
            else
                character.ChangeState<RheaIdleState>();
        }
        AudioManager.instance.Play("RheaIntrinsicShieldAway");
        HandleMoveAnimation(input);
    }

    override public void SoftTransitionWarning(RheaStateInput input)
    {
        GameObject.Destroy(shield);
    }

    override public void FixedUpdate(RheaStateInput input)
    {
        input.cc.HandleMovement();
    }
}

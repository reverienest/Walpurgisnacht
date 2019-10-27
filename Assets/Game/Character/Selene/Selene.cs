using System;
using UnityEngine;

public class Selene : Character<Selene, SeleneState, SeleneStateInput>
{
    [SerializeField]
    private float maxTravelTime = 1;
    [SerializeField]
    private float maxTravelDistance = 1;

    override protected void Init()
    {
        input.cc = GetComponent<SharedCharacterController>();
        input.rb = GetComponent<Rigidbody2D>();
        input.cc2d = GetComponent<CircleCollider2D>();
        input.anim = GetComponent<Animator>();

    }

    override protected void SetInitialState()
    {
        ChangeState<SeleneIdleState>();
    }

    override protected void UpdateInput()
    {
        input.maxTravelTime = maxTravelTime;
        input.maxTravelDistance = maxTravelDistance;
    }
}

public abstract class SeleneState : CharacterState<Selene, SeleneState, SeleneStateInput>
{
}

public class SeleneStateInput : CharacterStateInput
{
    public float maxTravelTime;
    public float maxTravelDistance;

    public SharedCharacterController cc;
    public Rigidbody2D rb;
    public CircleCollider2D cc2d;
    public Animator anim;
}
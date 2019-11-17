using System;
using UnityEngine;

public class Selene : Character<Selene, SeleneState, SeleneStateInput>
{
    // SeleneTeleportState constants
    [SerializeField]
    private float maxTravelTime = 1;
    [SerializeField]
    private float maxTravelDistance = 1;

    override protected void Init()
    {
        input.anim = GetComponent<Animator>();
        input.cc2d = GetComponent<CircleCollider2D>();
        input.rb = GetComponent<Rigidbody2D>();
        input.cc = GetComponent<SharedCharacterController>();
        input.sr = GetComponent<SpriteRenderer>();
        input.shot = GetComponent<SeleneFire>();
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

    public Animator anim;
    public CircleCollider2D cc2d;
    public Rigidbody2D rb;
    public SharedCharacterController cc;
    public SpriteRenderer sr;
    public SeleneFire shot;
}
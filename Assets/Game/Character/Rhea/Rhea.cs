using System;
using UnityEngine;

public class Rhea : Character<Rhea, RheaState, RheaStateInput>
{
    // SeleneTeleportState constants
    [SerializeField]
    private float maxTravelTime = 1;
    [SerializeField]
    private float maxTravelDistance = 1;
    [SerializeField]
    private GameObject shieldPrefab;

    override protected void Init()
    {
        input.anim = GetComponent<Animator>();
        input.cc2d = GetComponent<CircleCollider2D>();
        input.rb = GetComponent<Rigidbody2D>();
        input.cc = GetComponent<SharedCharacterController>();
        input.sr = GetComponent<SpriteRenderer>();
        input.shield = shieldPrefab;
    }

    override protected void SetInitialState()
    {
        ChangeState<RheaIdleState>();
    }

    override protected void UpdateInput()
    {
        input.maxTravelTime = maxTravelTime;
        input.maxTravelDistance = maxTravelDistance;
    }
}

public abstract class RheaState : CharacterState<Rhea, RheaState, RheaStateInput>
{
}

public class RheaStateInput : CharacterStateInput
{
    public float maxTravelTime;
    public float maxTravelDistance;

    public Animator anim;
    public CircleCollider2D cc2d;
    public Rigidbody2D rb;
    public SharedCharacterController cc;
    public SpriteRenderer sr;
    public GameObject shield;
}
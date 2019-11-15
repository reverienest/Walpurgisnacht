using System;
using UnityEngine;

public class Rhea : Character<Rhea, RheaState, RheaStateInput>
{
    // SeleneTeleportState constants
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private float timer = 0.2f;
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
        input.speed = speed;
        input.timer = timer;
    }
}

public abstract class RheaState : CharacterState<Rhea, RheaState, RheaStateInput>
{
}

public class RheaStateInput : CharacterStateInput
{
    public float speed;
    public float timer;

    public Animator anim;
    public CircleCollider2D cc2d;
    public Rigidbody2D rb;
    public SharedCharacterController cc;
    public SpriteRenderer sr;
    public GameObject shield;
}
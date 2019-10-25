using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SeleneIntrinsic : MonoBehaviour
{
    [SerializeField]
    private float maxTravelTime = 1;
    [SerializeField]
    private float maxTravelDistance = 1;

    private CharacterController cc;
    private Rigidbody2D rb;
    private CircleCollider2D cc2d;
    private Animator anim;

    private enum State { IDLE, ENTERING_HOLE, TRAVELING, EXITING_HOLE }
    private State state;

    //These variables are only for one state. Hmm... maybe we can account for this when making the big state machine.
    private float travelTime;
    private float travelTimer;
    private Vector2 travelOrigin;
    private Vector2 travelDestination;
    private Vector2 lastVelocity;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody2D>();
        cc2d = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();

        //Default teleport direction based on player number
        if (cc.playerNumber == 0)
            lastVelocity = Vector2.right;
        else
            lastVelocity = Vector2.left;
    }
    void Update()
    {
        switch(state)
        {
            case State.IDLE:
                if (rb.velocity.magnitude != 0)
                    lastVelocity = rb.velocity;

                if (SpellMap.Instance.GetSpellDown(cc.playerNumber, SpellType.INTRIN))
                {
                    travelOrigin = transform.position;

                    //Make sure we don't teleport out of the map
                    RaycastHit2D hit = Physics2D.Raycast(travelOrigin, lastVelocity, maxTravelDistance, LayerMask.GetMask("Arena"));
                    float travelDistance = hit.collider ? (hit.distance - cc2d.radius) : maxTravelDistance;
                    travelDestination = travelOrigin + lastVelocity.normalized * travelDistance;

                    travelTime = travelDistance / maxTravelDistance * maxTravelTime;
                    travelTimer = travelTime;

                    state = State.ENTERING_HOLE;
                    anim.Play("EnterHole");
                }
                break;

            case State.TRAVELING:
                travelTimer -= Time.deltaTime;
                transform.position = Vector2.Lerp(travelDestination, travelOrigin, travelTimer / travelTime);

                if (travelTimer <= 0)
                {
                    state = State.EXITING_HOLE;
                    anim.Play("ExitHole");
                }
                break;
        }
    }

    //Animation event callbacks
    public void OnHoleEntered()
    {
        switch (state)
        {
            case State.ENTERING_HOLE:
                state = State.TRAVELING;
                break;
        }
    }
    public void OnHoleExited()
    {
        switch (state)
        {
            case State.EXITING_HOLE:
                state = State.IDLE;
                break;
        }
    }
}

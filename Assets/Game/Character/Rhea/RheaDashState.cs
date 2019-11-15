using System;
using System.Collections.Generic;
using UnityEngine;

public class RheaDashState : RheaState
{
    private enum Sequence { START_DASH, TRAVEL, END_DASH }
    private Sequence state;

    private float travelTimer;
    private float speed;
    private Vector3 direction;


    override public void Enter(RheaStateInput input, CharacterStateTransitionInfo transitionInfo = null)
    {
        direction = input.cc.LastDirection.normalized;
        travelTimer = input.timer;
        speed = input.speed;

        //state = Sequence.START_DASH;
        //input.anim.Play("StartDash");

        //Delete later when animation is implemented
        state = Sequence.TRAVEL;
    }

    override public void Update(RheaStateInput input)
    {
        if (state == Sequence.TRAVEL)
        {
            travelTimer -= Time.deltaTime;
            character.transform.position += direction * Time.deltaTime * speed;
            List<Collider2D> contactList = new List<Collider2D>();
            input.cc2d.OverlapCollider(new ContactFilter2D(), contactList);
            bool collidesWithWall = false;
            foreach (Collider2D collider in contactList)
            {
                if (collider.gameObject.TryGetComponent<InnerCollider>(out InnerCollider component))
                {
                    collidesWithWall = true;
                    break;
                }
            }
            if (travelTimer <= 0 || collidesWithWall)
            {
                //travelTimer = input.timer;
                //state = Sequence.END_DASH;
                //input.anim.Play("EndDash");

                //Delete later when animation is implemented
                travelTimer = input.timer;
                character.ChangeState<RheaIdleState>();
            }

        }
    }

    override public void SoftTransitionWarning(RheaStateInput input)
    {
        //travelTimer =input.timer;
        //state = Sequence.END_DASH;
        //input.anim.Play("EndDash");

        //Delete later when animation is implemented
        travelTimer = input.timer;
        character.ChangeState<RheaIdleState>();
    }

    override public void OnAnimationEvent(string eventName)
    {
        if (eventName == "DashStarted" && state == Sequence.START_DASH)
        {
            state = Sequence.TRAVEL;
        }
        else if (eventName == "DashEnded" && state == Sequence.END_DASH)
        {
            if (character.softTransitionChangeState != null)
            {
                character.softTransitionChangeState();
            }
            else
            {
                character.ChangeState<RheaIdleState>();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class RheaDashState : RheaState
{
    private float travelTimer;
    private float dashSpeed;
    private Vector3 direction;

    override public void Enter(RheaStateInput input, CharacterStateTransitionInfo transitionInfo = null)
    {
        input.slowSprite.enabled = false;

        direction = input.cc.LastDirection.normalized;
        travelTimer = input.dashDuration;
        dashSpeed = input.dashSpeed;
        
        AudioManager.instance.Play("RheaDash");
        input.anim.Play("Dash");
    }

    override public void Update(RheaStateInput input)
    {
        input.slowSprite.enabled = false;
        travelTimer -= Time.deltaTime;
        character.transform.position += direction * Time.deltaTime * dashSpeed;
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
            character.ChangeState<RheaIdleState>();
        }
    }
}

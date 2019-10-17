using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    /// <summary>
    /// This is zero indexed
    /// </summary>
    public int playerNumber;

    [SerializeField]
    private float speed = 7;
    private float slowSpeed = 3;

    private Vector3 velocity;

    void Update()
    {
        int horizontalMovement = 0;
        if (InputMap.Instance.GetInput(playerNumber, Action.RIGHT))
        {
            horizontalMovement++;
        }
        if (InputMap.Instance.GetInput(playerNumber, Action.LEFT))
        {
            horizontalMovement--;
        }
        int verticalMovement = 0;
        if (InputMap.Instance.GetInput(playerNumber, Action.UP))
        {
            verticalMovement++;
        }
        if (InputMap.Instance.GetInput(playerNumber, Action.DOWN))
        {
            verticalMovement--;
        }

        velocity = new Vector3(horizontalMovement, verticalMovement, 0);
        if (InputMap.Instance.GetInput(playerNumber, Action.SLOW))
        {
            transform.position += velocity.normalized * Time.deltaTime * slowSpeed;
        }
        else
        {
            transform.position += velocity.normalized * Time.deltaTime * speed;
        }
    }


}

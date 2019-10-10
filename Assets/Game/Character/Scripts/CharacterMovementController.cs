using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField]
    private float speed = 7;

    private Vector3 velocity;

    void Update()
    {
        int horizontalMovement = 0;
        if (InputMap.Instance.GetInput(Action.RIGHT))
        {
            horizontalMovement++;
        }
        if (InputMap.Instance.GetInput(Action.LEFT))
        {
            horizontalMovement--;
        }
        int verticalMovement = 0;
        if (InputMap.Instance.GetInput(Action.UP))
        {
            verticalMovement++;
        }
        if (InputMap.Instance.GetInput(Action.DOWN))
        {
            verticalMovement--;
        }

        velocity = new Vector3(horizontalMovement, verticalMovement, 0);
        transform.position += velocity.normalized * Time.deltaTime * speed;
    }


}

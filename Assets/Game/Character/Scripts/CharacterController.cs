using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    /// <summary>
    /// This is zero indexed
    /// </summary>
    public int playerNumber;

    [SerializeField]
    private float speed = 7;
    [SerializeField]
    private float slowSpeed = 3;

    [SerializeField]
    private GameObject circlePrefab = null;

    private GameObject magicCircle;

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


        if (InputMap.Instance.GetInput(playerNumber, Action.CAST_CIRCLE) && null == magicCircle)
        {
            float height = this.gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
            Vector2 circleSpawnLocation = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - (height / 2));
            magicCircle = Instantiate(circlePrefab, circleSpawnLocation, Quaternion.identity);
            magicCircle.GetComponent<MagicCircle>().SpawnCircle(playerNumber);
        }
    }


}

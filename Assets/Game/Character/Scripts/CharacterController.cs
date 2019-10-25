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

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (InputMap.Instance.GetInput(playerNumber, ActionType.CAST_CIRCLE) && null == magicCircle)
        {
            float height = this.gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
            Vector2 circleSpawnLocation = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - (height / 2));
            magicCircle = Instantiate(circlePrefab, circleSpawnLocation, Quaternion.identity);
            magicCircle.GetComponent<MagicCircle>().SpawnCircle(playerNumber);
        }
    }

    void FixedUpdate()
    {
        int horizontalMovement = 0;
        if (InputMap.Instance.GetInput(playerNumber, ActionType.RIGHT))
        {
            horizontalMovement++;
        }
        if (InputMap.Instance.GetInput(playerNumber, ActionType.LEFT))
        {
            horizontalMovement--;
        }
        int verticalMovement = 0;
        if (InputMap.Instance.GetInput(playerNumber, ActionType.UP))
        {
            verticalMovement++;
        }
        if (InputMap.Instance.GetInput(playerNumber, ActionType.DOWN))
        {
            verticalMovement--;
        }

        rb.velocity = new Vector2(horizontalMovement, verticalMovement);
        if (InputMap.Instance.GetInput(playerNumber, ActionType.SLOW))
        {
            transform.position += (Vector3)rb.velocity.normalized * Time.deltaTime * slowSpeed;
        }
        else
        {
            transform.position += (Vector3)rb.velocity.normalized * Time.deltaTime * speed;
        }
    }
}

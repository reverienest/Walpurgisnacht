using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Handles movement and placing magic circle
public class SharedCharacterController : MonoBehaviour
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
    [SerializeField]
    private SpriteRenderer hitboxSR = null;

    private GameObject magicCircle;
    public bool HasCircle() { return magicCircle != null; }
    public Vector2 GetCirclePosition() { return magicCircle.transform.position; }

    private Rigidbody2D rb;

    private Vector2 lastDirection;
    /// Last non-zero direction this character moved
    public Vector2 LastDirection { get { return lastDirection; } }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        MatchManager.Instance.OnLastWordStart += OnLastWordStart;
        MatchManager.Instance.OnLastWordEnd += OnLastWordEnd;
    }

    void Start()
    {
        hitboxSR.color = MatchManager.Instance.GetPlayerOutlineColor(playerNumber);

        //Default teleport direction based on player number
        if (playerNumber == 0)
            lastDirection = Vector2.right;
        else
            lastDirection = Vector2.left;
    }

    public bool HandlePlaceCircle()
    {
        if (InputMap.Instance.GetInput(playerNumber, ActionType.CAST_CIRCLE) //are they pressing the button?
            && null == magicCircle //have they already cast the circle?
            && !MatchManager.Instance.LastWordActive //is either player using their last word?
            && SpellMap.Instance.SpellReady(playerNumber, SpellType.CIRCLE)) //is the cast on cooldown?
        {
            float height = this.gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
            Vector2 circleSpawnLocation = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - (height / 2));
            magicCircle = Instantiate(circlePrefab, circleSpawnLocation, Quaternion.identity);
            magicCircle.GetComponent<MagicCircle>().SpawnCircle(playerNumber);
            return true;
        }

        return false;
    }

    public bool HandleLastWord()
    {
        if (InputMap.Instance.GetInput(playerNumber, ActionType.LAST_WORD) //are they pressing the button?
            && PlayerStatsManager.Instance.PlayerAtFullMP(playerNumber) //are they at full MP?
            && magicCircle.GetComponent<MagicCircle>().IsPlayerInCircle //are they in the circle?
            && !MatchManager.Instance.LastWordActive ) //is other player using their last word?
        {
            MatchManager.Instance.StartLastWord(playerNumber);
            RemoveCircle();

            if (0 == playerNumber)
            {
                PlayerStatsManager.Instance.Player1MP = 0;
            }
            else
            {
                PlayerStatsManager.Instance.Player2MP = 0;
            }
            return true;
        }

        return false;
    }

    private bool movementHandled;
    public void HandleMovement()
    {
        movementHandled = true;
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

        if (InputMap.Instance.GetInput(playerNumber, ActionType.SLOW))
        {
            rb.velocity = new Vector2(horizontalMovement, verticalMovement).normalized * slowSpeed;
            hitboxSR.enabled = true;
        }
        else
        {
            rb.velocity = new Vector2(horizontalMovement, verticalMovement).normalized * speed;
            hitboxSR.enabled = false;
        }

        if (rb.velocity.magnitude != 0)
            lastDirection = rb.velocity.normalized;
    }

    void FixedUpdate()
    {
        if (!movementHandled)
            rb.velocity = Vector2.zero;
        movementHandled = false;
    }

    public void RemoveCircle()
    {
        if (null != magicCircle)
            Destroy(magicCircle);
    }

    private void OnLastWordStart(int lastWordCastBy)
    {
        if (lastWordCastBy == playerNumber || null == magicCircle)
            return;

        // the other player has cast their last word, disable our circle
        magicCircle.SetActive(false);
    }


    private void OnLastWordEnd()
    {
        // last word has ended, try to enable our circle
        if (null == magicCircle)
            return;

        magicCircle.SetActive(true);

    }
}

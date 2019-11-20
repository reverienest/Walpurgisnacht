using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    /// <summary>
    /// This is zero indexed
    /// </summary>
    public int playerNumber;

    [SerializeField]
    private Sprite[] circleSprites = new Sprite[2];

    [SerializeField]
    private int circleChargeTime = 0;

    [SerializeField]
    private int startingMP = 0;

    [SerializeField]
    private int timePerWardRecharge = 0;

    [SerializeField]
    private int mpPerWardRecharge = 0;

    private float timeModifierMPCharge;
    private float lastUpdateMPCharge = -1;

    private float timeModifierWardRefill;
    private float wardRefillModifier;
    private float slowdownWhenNoMP;
    private float lastWardRefillTime = -1;
    private float lastWardRefillValue = 0;
    private float lastUpdateMPDischarge = 0;

    private int restitutionMP = 0;

    // Start is called before the first frame update
    void Start()
    {
        CharacterType type = CharacterSelection.Instance.GetPlayerCharacterType(playerNumber);
        GetComponent<SpriteRenderer>().sprite = circleSprites[(int)type];

        //time it takes to increase MP by 1 during normal play
        timeModifierMPCharge = (float)circleChargeTime / (PlayerStatsManager.Instance.MaxMP - startingMP);

        //time it takes to decrease MP by 1 when recharging a ward
        timeModifierWardRefill = (float)timePerWardRecharge / mpPerWardRecharge;

        //normalized ward refill per second
        wardRefillModifier = 1.0f / timePerWardRecharge;

        //multiplier to addedRefill when player has drained all their MP
        //basically, what percentage of the overall decrease specified by the serialized fields does the natural recharge contribute?
        slowdownWhenNoMP = (1.0f / timeModifierMPCharge) / ((1.0f / timeModifierMPCharge) + (1.0f / timeModifierWardRefill));
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldRefill)
        {
            float addedRefill = (Time.time - lastWardRefillTime) * wardRefillModifier;

            if (0 == GetGuage())
            {
                addedRefill *= slowdownWhenNoMP;
            }

            float newWardRefillValue = lastWardRefillValue + addedRefill;
            SetWardRefill(newWardRefillValue);

            if (newWardRefillValue > 1)
            {
                //ward refilled
                restitutionMP = GetGuage();
                newWardRefillValue = 0;
            }

            if (Time.time - lastUpdateMPDischarge >= timeModifierWardRefill)
            {
                lastUpdateMPDischarge = Time.time;
                DecrementGuage();
            }

            if (Time.time - lastUpdateMPCharge >= timeModifierMPCharge)
            {
                lastUpdateMPCharge = Time.time;
                restitutionMP += 1;
            }

            lastWardRefillTime = Time.time;
            lastWardRefillValue = newWardRefillValue;
        }
        else if (IsCircleSpawned)
        {
            if (Time.time - lastUpdateMPCharge >= timeModifierMPCharge)
            {
                lastUpdateMPCharge = Time.time;
                IncrementGuage();
            }
        }

    }

    public bool IsPlayerInCircle
    {
        get => lastWardRefillTime != -1;
    }

    private bool IsCircleSpawned
    {
        get => lastUpdateMPCharge != -1;
    }

    private bool ShouldRefill
    {
        get
        {
            if (!IsPlayerInCircle) { return false; }


            if (playerNumber == 0)
            {
                return PlayerStatsManager.Instance.Player1Wards < PlayerStatsManager.Instance.Player1Health;
            }
            else
            {
                return PlayerStatsManager.Instance.Player2Wards < PlayerStatsManager.Instance.Player2Health;
            }
        }
    }

    public void SpawnCircle(int playerNumber)
    {
        this.playerNumber = playerNumber;
        lastUpdateMPCharge = Time.time;
        SetGuage(startingMP);
        SpellMap.Instance.HideCircleCooldown(playerNumber);
    }


    private void SetWardRefill(float refillValue)
    {
        if (playerNumber == 0)
        {
            PlayerStatsManager.Instance.Player1WardRefill = refillValue;
        }
        else
        {
            PlayerStatsManager.Instance.Player2WardRefill = refillValue;
        }
    }

    private int GetGuage()
    {
        if (playerNumber == 0)
        {
            return PlayerStatsManager.Instance.Player1MP;
        }
        else
        {
            return PlayerStatsManager.Instance.Player2MP;
        }
    }

    private void SetGuage(int percentage)
    {
        if (playerNumber == 0)
        {
            PlayerStatsManager.Instance.Player1MP = percentage;
        }
        else
        {
            PlayerStatsManager.Instance.Player2MP = percentage;
        }
    }

    private void IncrementGuage()
    {
        if (playerNumber == 0)
        {
            PlayerStatsManager.Instance.Player1MP += 1;
        }
        else
        {
            PlayerStatsManager.Instance.Player2MP += 1;
        }
    }

    private void DecrementGuage()
    {
        if (playerNumber == 0)
        {
            PlayerStatsManager.Instance.Player1MP -= 1;
        }
        else
        {
            PlayerStatsManager.Instance.Player2MP -= 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsPlayerInCircle)
        {
            //player has not left circle since entering
            return;
        }

        if (playerNumber != collision.gameObject.GetComponentInParent<SharedCharacterController>().playerNumber)
        {
            //not the player who cast this circle
            return;
        }

        if (playerNumber == 0)
        {
            restitutionMP = PlayerStatsManager.Instance.Player1MP;
        }
        else
        {
            restitutionMP = PlayerStatsManager.Instance.Player2MP;
        }

        lastUpdateMPDischarge = Time.time;
        lastWardRefillTime = Time.time;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (playerNumber != collision.gameObject.GetComponentInParent<SharedCharacterController>().playerNumber)
        {
            //not the same player
            return;
        }

        if (restitutionMP > GetGuage())
        {
            SetGuage(restitutionMP);
        }

        lastWardRefillValue = 0;
        lastWardRefillTime = -1;

        SetWardRefill(0);
    }
}

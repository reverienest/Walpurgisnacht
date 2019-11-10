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
    private int circleChargeTime = 0;

    [SerializeField]
    private int startingMP = 0;

    [SerializeField]
    private float timePerWard = 0;

    private float timeModifierMPCharge;
    private float lastUpdateMPCharge = -1;

    private float lastUpdateWardRefill = -1;
    private float lastWardRefillValue = 0;

    private int restitutionMP = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlayerStatsManager.Instance.Player1Wards = 0;
        timeModifierMPCharge = ((float)circleChargeTime) / (PlayerStatsManager.Instance.MaxMP - startingMP);
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldRefill)
        {
            float addedRefill = (Time.time - lastUpdateWardRefill) / timePerWard;

            if (0 == GetGuage())
            {
                addedRefill /= 2;
            }

            float newWardRefillValue = lastWardRefillValue + addedRefill;
            SetWardRefill(newWardRefillValue);
            
            if (newWardRefillValue > 1)
            {
                //ward refilled
                restitutionMP -= Mathf.FloorToInt(100 * timePerWard / circleChargeTime);
                newWardRefillValue = 0;
            }

            if (Time.time - lastUpdateMPCharge >= timeModifierMPCharge)
            {
                lastUpdateMPCharge = Time.time;
                DecrementGuage();
                restitutionMP += 1;
            }

            lastUpdateWardRefill = Time.time;
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

    private bool IsCircleSpawned
    {
        get => lastUpdateMPCharge != -1;
    }

    private bool IsPlayerInCircle
    {
        get => lastUpdateWardRefill != -1;
    }

    private bool ShouldRefill
    {
        get
        {
            if (!IsPlayerInCircle) { return false; }


            if (playerNumber == 0)
            {
                return PlayerStatsManager.Instance.Player1Wards < PlayerStatsManager.Instance.MaxHealth;
            }
            else
            {
                return PlayerStatsManager.Instance.Player2Wards < PlayerStatsManager.Instance.MaxHealth;
            }
        }
    }

    public void SpawnCircle(int playerNumber)
    {
        this.playerNumber = playerNumber;
        lastUpdateMPCharge = Time.time;
        SetGuage(startingMP);
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

        lastUpdateWardRefill = Time.time;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (playerNumber != collision.gameObject.GetComponentInParent<SharedCharacterController>().playerNumber)
        {
            //not the same player
            return;
        }
        
        SetGuage(restitutionMP);

        lastWardRefillValue = 0;
        lastUpdateWardRefill = -1;

        SetWardRefill(0);
    }
}

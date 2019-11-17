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

    private float timeModifier;
    private float lastUpdate = -1;

    // Start is called before the first frame update
    void Start()
    {
        timeModifier =  ((float)circleChargeTime) / (PlayerStatsManager.Instance.MaxMP - startingMP);
    }

    // Update is called once per frame
    void Update()
    {
        if (lastUpdate != -1) // make sure SpawnCircle called
        {
            if (Time.time - lastUpdate >= timeModifier)
            {
                lastUpdate = Time.time;
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
        lastUpdate = Time.time;
        SetGuage(startingMP);
        SpellMap.Instance.HideCircleCooldown(playerNumber);
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
}

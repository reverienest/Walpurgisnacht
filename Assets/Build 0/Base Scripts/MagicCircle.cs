using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicCircle : MonoBehaviour
{    
    /// <summary>
     /// This is zero indexed
     /// </summary>
    public int playerNumber;

    private GameObject MagicCircleSprite;

    private float spawnTime = -1; 

    private static readonly int startingMP = 25;
    private static readonly int chargeTime = 10;
    private static readonly float timeModifier = (100f - startingMP) / chargeTime;

    void Update()
    {
        if (spawnTime != -1) //make sure SpawnCircle called
        {
            int newGuageValue = Mathf.FloorToInt((Time.time - spawnTime) * timeModifier) + startingMP;
            SetGuage(Mathf.Min(100, newGuageValue));
        }
    }

    public void SpawnCircle(int playerNumber)
    {
        this.playerNumber = playerNumber;
        spawnTime = Time.time;
        SetGuage(startingMP);

        //TODO: show circle sprite here
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
}

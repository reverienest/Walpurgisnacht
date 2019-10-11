using UnityEngine;
using System.Collections;

public class MagicCircleManager : Singleton<MagicCircleManager>
{
    [SerializeField]
    private int startingMP = 1;

    [SerializeField]
    private int chargeTime = 1;

    private float timeModifier;

    [SerializeField]
    private GameObject magicCircleSpritePrefab = null;

    private GameObject player1MagicCircle = null;
    private GameObject player2MagicCircle = null;

    private float player1CircleSpawnTime;
    private float player2CircleSpawnTime;
    
    void Start()
    {
        timeModifier =  (100f - startingMP) / chargeTime;
    }
    
    void Update()
    {
        if (player1MagicCircle != null) //make sure SpawnCircle called
        {
            int newGuageValue = Mathf.FloorToInt((Time.time - player1CircleSpawnTime) * timeModifier) + startingMP;
            PlayerStatsManager.Instance.Player1MP = Mathf.Min(100, newGuageValue);
        }

        if (player2MagicCircle != null) //make sure SpawnCircle called
        {
            int newGuageValue = Mathf.FloorToInt((Time.time - player2CircleSpawnTime) * timeModifier) + startingMP;
            PlayerStatsManager.Instance.Player2MP = Mathf.Min(100, newGuageValue);
        }

    }

    public void SpawnCircle(int playerNumber, Vector2 playerPosition)
    {
        if (playerNumber == 0)
        {
            player1CircleSpawnTime = Time.time;
            PlayerStatsManager.Instance.Player1MP = startingMP;

            player1MagicCircle = Instantiate(
                magicCircleSpritePrefab,
                playerPosition,
                Quaternion.identity);
        }
        else
        {
            player2CircleSpawnTime = Time.time;
            PlayerStatsManager.Instance.Player1MP = startingMP;

            player2MagicCircle = Instantiate(
                magicCircleSpritePrefab,
                playerPosition,
                Quaternion.identity);
        }
    }
}

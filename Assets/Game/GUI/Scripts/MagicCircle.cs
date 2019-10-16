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
    private int startingMP = 1;

    [SerializeField]
    private int chargeTime = 1;

    private float timeModifier;
    private float spawnTime = -1; 	

    // Start is called before the first frame update
    void Start()
    {
        timeModifier =  (100f - startingMP) / chargeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTime != -1) // make sure SpawnCircle called	
        {	
            int newGuageValue = Mathf.FloorToInt((Time.time - spawnTime) * timeModifier) + startingMP;	
            SetGuage(Mathf.Min(100, newGuageValue));	
        }	
    }

    public void SpawnCircle(int playerNumber)	
    {	
        this.playerNumber = playerNumber;	
        spawnTime = Time.time;	
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

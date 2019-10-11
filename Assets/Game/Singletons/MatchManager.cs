﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchManager : Singleton<MatchManager>
{
    [SerializeField]
    private CharacterType player1CharacterType = default;
    [SerializeField]
    private CharacterType player2CharacterType = default;

    [SerializeField]
    private Transform player1SpawnPoint = null;
    [SerializeField]
    private Transform player2SpawnPoint = null;

    [SerializeField]
    private GameObject[] characterPrefabs = null;

    [SerializeField]
    private int winsNeeded = 3;

    [SerializeField]
    private Text[] playerScoreTexts = null;

    private int[] _playerScores = new int[2];
    private void SetPlayerScore(int playerNumber, int value)
    {
        _playerScores[playerNumber] = value;
        playerScoreTexts[playerNumber].text = _playerScores[playerNumber].ToString();
    }

    private GameObject player1;
    private GameObject player2;

    void Start()
    {
        PlayerStatsManager.Instance.OnDeathAction += new PlayerStatsManager.DeathAction(OnPlayerDeath);
        ResetArena();
    }

    private void ResetArena()
    {
        //Reset stats
        PlayerStatsManager.Instance.Start();

        //Cleanup old players
        if (player1)
            Destroy(player1);
        if (player2)
            Destroy(player2);

        //Spawn new players
        player1 = Instantiate(
            characterPrefabs[(int)player1CharacterType],
            player1SpawnPoint.position,
            Quaternion.identity);
        player2 = Instantiate(
            characterPrefabs[(int)player2CharacterType],
            player2SpawnPoint.position,
            Quaternion.identity);

        //Setup parameters for their tracking and controls
        player1.GetComponent<CharacterTargeting>().target = player2.transform;
        player1.GetComponent<CharacterMovementController>().playerNumber = 0;
        player2.GetComponent<CharacterTargeting>().target = player1.transform;
        player2.GetComponent<CharacterMovementController>().playerNumber = 1;
    }

    private void OnPlayerDeath(int playerNumber)
    {
        int alivePlayer = playerNumber == 0 ? 1 : 0;
        SetPlayerScore(alivePlayer, _playerScores[alivePlayer] + 1);

        if (_playerScores[alivePlayer] == winsNeeded)
        {
            //TODO: Go to the victory screen (once we have it)
        }
        else
        {
            ResetArena();
        }
    }
}

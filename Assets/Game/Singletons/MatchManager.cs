using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MatchManager : Singleton<MatchManager>
{
    [SerializeField]
    private CharacterType[] playerCharacterTypes = new CharacterType[2];
    public CharacterType GetPlayerCharacterType(int playerNumber)
    {
        return playerCharacterTypes[playerNumber];
    }

    [SerializeField]
    private Transform player1SpawnPoint = null;
    [SerializeField]
    private Transform player2SpawnPoint = null;

    [SerializeField]
    private GameObject[] characterPrefabs = null;
    [SerializeField]
    private Sprite[] characterHeadshots = null;

    [SerializeField]
    private int winsNeeded = 3;
    [SerializeField]
    private string roundStartString = "Get em'!";
    [SerializeField]
    private string victorySceneName = null;

    [SerializeField]
    private Text[] playerScoreTexts = null;
    [SerializeField]
    private Image[] playerCharacterImages = null;

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
        //Setup player character UI images
        for (int i = 0; i < playerCharacterImages.Length; ++i)
            playerCharacterImages[i].sprite = characterHeadshots[(int)playerCharacterTypes[i]];

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
            characterPrefabs[(int)playerCharacterTypes[0]],
            player1SpawnPoint.position,
            Quaternion.identity);
        player2 = Instantiate(
            characterPrefabs[(int)playerCharacterTypes[1]],
            player2SpawnPoint.position,
            Quaternion.identity);

        //Setup parameters for their tracking and controls
        player1.GetComponent<CharacterTargeting>().target = player2.transform;
        player1.GetComponent<SharedCharacterController>().playerNumber = 0;
        player2.GetComponent<CharacterTargeting>().target = player1.transform;
        player2.GetComponent<SharedCharacterController>().playerNumber = 1;

        SplashTextManager.Instance.Splash(roundStartString, () => InputMap.Instance.inputEnabled = true);
    }

    private void OnPlayerDeath(int playerNumber)
    {
        int alivePlayer = playerNumber == 0 ? 1 : 0;
        SetPlayerScore(alivePlayer, _playerScores[alivePlayer] + 1);

        //Round winner splash text
        string aliveCharacterString = GetPlayerCharacterType(0).ToString();
        aliveCharacterString = aliveCharacterString.Substring(0, 1).ToUpper()
            + aliveCharacterString.Substring(1).ToLower();
        string victoryString = aliveCharacterString + " wins!";

        if (_playerScores[alivePlayer] == winsNeeded)
        {
            SplashTextManager.Instance.Splash(victoryString, () => SceneManager.LoadScene(victorySceneName));
        }
        else
        {
            InputMap.Instance.inputEnabled = false;

            SplashTextManager.Instance.Splash(victoryString, ResetArena);

            //TODO: Start some transition animation

            // ResetArena();
        }
    }
}

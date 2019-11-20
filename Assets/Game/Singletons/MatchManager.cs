using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MatchManager : Singleton<MatchManager>
{
    public delegate void VictoryAction(int playerNumber);
    public event VictoryAction OnVictoryAction;

    public delegate void LastWordStartAction(int playerNumber);
    public event LastWordStartAction OnLastWordStart;

    public delegate void LastWordEndAction();
    public event LastWordEndAction OnLastWordEnd;

    public delegate void LastWordTimerChangeAction(float oldValue, float newValue);
    public event LastWordTimerChangeAction OnLastWordTimerChange;

    [SerializeField]
    private Transform[] spawnPoints = new Transform[2];

    [SerializeField]
    private GameObject[] characterPrefabs = null;
    [SerializeField]
    private Sprite[] characterHeadshots = null;

    [SerializeField]
    private int winsNeeded = 3;
    [SerializeField]
    private string roundStartString = "Get em'!";

    [SerializeField]
    private float lastWordDuration = 30;
    [SerializeField]
    private float lastWordMoveTime = 1;
    [SerializeField]
    private Transform[] lastWordPositions = new Transform[2];

    [SerializeField]
    private Text[] playerScoreTexts = null;
    [SerializeField]
    private Image[] playerCharacterImages = null;
    [SerializeField]
    private Color[] playerOutlineColors = new Color[2];
    public Color GetPlayerOutlineColor(int playerNumber) { return playerOutlineColors[playerNumber]; }

    private int[] _playerScores = new int[2];
    private void SetPlayerScore(int playerNumber, int value)
    {
        _playerScores[playerNumber] = value;
        playerScoreTexts[playerNumber].text = _playerScores[playerNumber].ToString();
    }
    public int GetPlayerScore(int playerNumber)
    {
        return _playerScores[playerNumber];
    }

    private GameObject[] players = new GameObject[2];
    public GameObject[] Players { get { return players; } }

    private int lastWordPlayerNumber;
    private bool lastWordActive;
    public bool LastWordActive { get { return lastWordActive; } }
    private float _lastWordTimer;
    private float LastWordTimer
    {
        get
        {
            return _lastWordTimer;
        }
        set
        {
            OnLastWordTimerChange?.Invoke(_lastWordTimer, value);
            _lastWordTimer = value;
        }
    }
    private ForceMoveTransitionInfo.AllInPositionAction[] playersOnAllInPosition = new ForceMoveTransitionInfo.AllInPositionAction[2];
    private Coroutine lastWordCoroutine;

    void Start()
    {
        // Setup player character UI images
        for (int i = 0; i < playerCharacterImages.Length; ++i)
            playerCharacterImages[i].sprite = characterHeadshots[(int)CharacterSelection.Instance.GetPlayerCharacterType(i)];

        PlayerStatsManager.Instance.OnDeathAction += new PlayerStatsManager.DeathAction(OnPlayerDeath);
        InputMap.Instance.inputEnabled = false;
        ResetArena();
    }

    public void StartLastWord(int lastWordPlayerNumber)
    {
        if (lastWordCoroutine != null)
            StopCoroutine(lastWordCoroutine);

        this.lastWordPlayerNumber = lastWordPlayerNumber;
        lastWordActive = true;
        LastWordTimer = lastWordDuration;
        int victimPlayerNumber = lastWordPlayerNumber == 0 ? 1 : 0;

        ClearBullets();
        PlayerStatsManager.Instance.StashWards(victimPlayerNumber);
        OnLastWordStart?.Invoke(lastWordPlayerNumber);
        StartForcedMovement(true);
    }

    private void ClearBullets()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Destroy(g);
        }
    }

    private void StartForcedMovement(bool isStartingLastWord)
    {
        for (int playerNum = 0; playerNum < players.Length; ++playerNum)
        {
            int playerNumCopy = playerNum;
            ForceMoveTransitionInfo transitionInfo = new ForceMoveTransitionInfo
            {
                onCompleteMove = (ForceMoveTransitionInfo.AllInPositionAction onAllInPosition) => OnCompleteMove(playerNumCopy, isStartingLastWord, onAllInPosition),
                isLastWordCaster = isStartingLastWord && playerNum == lastWordPlayerNumber,
                moveTime = lastWordMoveTime,
            };

            SharedCharacterController cc = players[playerNum].GetComponent<SharedCharacterController>();
            if (isStartingLastWord)
                transitionInfo.moveDestination = lastWordPositions[playerNum].position;
            else if (cc.HasCircle())
                transitionInfo.moveDestination = cc.GetCirclePosition();
            else
                transitionInfo.moveDestination = spawnPoints[playerNum].position;

            switch (CharacterSelection.Instance.GetPlayerCharacterType(playerNum))
            {
                case CharacterType.SELENE:
                    Selene selene = players[playerNum].GetComponent<Selene>();
                    selene.ChangeStateSoft<SeleneForceMoveState>(transitionInfo);
                    break;
                case CharacterType.RHEA:
                    Rhea rhea = players[playerNum].GetComponent<Rhea>();
                    rhea.ChangeStateSoft<RheaForceMoveState>(transitionInfo);
                    break;
            }
        }
    }

    /// Callback for when a character completes moving to/from its last word position
    private void OnCompleteMove(int playerNumber, bool isStartingLastWord, ForceMoveTransitionInfo.AllInPositionAction onAllInPosition)
    {
        playersOnAllInPosition[playerNumber] = onAllInPosition;

        // Must wait until all players are in position until we proceed
        if (playersOnAllInPosition[0] != null && playersOnAllInPosition[1] != null)
        {
            playersOnAllInPosition[0]();
            playersOnAllInPosition[1]();

            if (isStartingLastWord)
                lastWordCoroutine = StartCoroutine(DuringLastWord());

            playersOnAllInPosition[0] = null;
            playersOnAllInPosition[1] = null;
        }
    }

    private IEnumerator DuringLastWord()
    {
        while (LastWordTimer > 0)
        {
            LastWordTimer -= Time.deltaTime;
            yield return null;
        }
        EndLastWord();
    }

    private void EndLastWord()
    {
        lastWordActive = false;
        int victimPlayerNumber = lastWordPlayerNumber == 0 ? 1 : 0;

        ClearBullets();
        PlayerStatsManager.Instance.PopWards(victimPlayerNumber);
        OnLastWordEnd?.Invoke();
        StartForcedMovement(false);
    }

    private void ResetArena()
    {
        ClearBullets();

        // Handles edge case of game ending during Last Word
        if (lastWordActive)
        {
            StopCoroutine(lastWordCoroutine);
            EndLastWord();
        }

        // Reset stats
        PlayerStatsManager.Instance.Start();
        SpellMap.Instance.ResetAllCooldowns();

        for (int playerNum = 0; playerNum < players.Length; ++playerNum)
        {
            // Cleanup old players
            if (players[playerNum])
            {
                players[playerNum].GetComponent<SharedCharacterController>().RemoveCircle();
                SpellMap.Instance.ShowCircleCooldown(playerNum); // HACK: if they lose while their circle is cast this won't be called naturally
                Destroy(players[playerNum]);
            }

            // Spawn new players
            players[playerNum] = Instantiate(
                characterPrefabs[(int)CharacterSelection.Instance.GetPlayerCharacterType(playerNum)],
                spawnPoints[playerNum].position,
                Quaternion.identity);

            players[playerNum].transform.Find("Face").gameObject.SetActive(GlobalSettingsManager.Instance.CageMode); // ???
        }

        // Setup parameters for their tracking and controls
        players[0].GetComponent<CharacterTargeting>().target = players[1].transform;
        players[0].GetComponent<SharedCharacterController>().playerNumber = 0;
        players[0].GetComponent<SpriteOutline>().color = playerOutlineColors[0];
        players[1].GetComponent<CharacterTargeting>().target = players[0].transform;
        players[1].GetComponent<SharedCharacterController>().playerNumber = 1;
        players[1].GetComponent<SpriteOutline>().color = playerOutlineColors[1];

        SplashTextManager.Instance.Splash(roundStartString, () => InputMap.Instance.inputEnabled = true);
    }

    private void OnPlayerDeath(int playerNumber)
    {
        int alivePlayer = playerNumber == 0 ? 1 : 0;
        SetPlayerScore(alivePlayer, _playerScores[alivePlayer] + 1);

        InputMap.Instance.inputEnabled = false;

        // Round winner splash text
        string aliveCharacterString = CharacterSelection.Instance.GetPlayerCharacterType(alivePlayer).GetString();
        string victoryString = "(P" + (alivePlayer + 1) + ") " + aliveCharacterString + " wins!";

        if (_playerScores[alivePlayer] == winsNeeded)
        {
            SplashTextManager.Instance.Splash(victoryString, () => OnVictoryAction?.Invoke(alivePlayer));
        }
        else
        {
            SplashTextManager.Instance.Splash(victoryString, ResetArena);
        }
    }
}

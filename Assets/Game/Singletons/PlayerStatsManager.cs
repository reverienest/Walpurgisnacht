using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsManager : Singleton<PlayerStatsManager>
{
    /*
    * UI components set by Unity
    */

    [SerializeField]
    private int maxHealth = 0;
    [SerializeField]
    private int maxWards = 0;

    [SerializeField]
    private int maxMP = 0;
    [SerializeField]
    private int startingMP = 0;

    public int MaxMP { get { return maxMP; } }
    public int StartingMP { get { return startingMP; } }

    [SerializeField]
    private GameObject[] player1HeartIcons = null;
    [SerializeField]
    private GameObject[] player1WardIcons = null;
    [SerializeField]
    private Slider player1MPSlider = null;

    [SerializeField]
    private GameObject[] player2HeartIcons = null;
    [SerializeField]
    private GameObject[] player2WardIcons = null;
    [SerializeField]
    private Slider player2MPSlider = null;

    // private backing variables

    private int _player1Health;
    private int _player1Wards;
    private int _player1MP;

    private int _player2Health;
    private int _player2Wards;
    private int _player2MP;

    // public properties

    public int Player1Health
    {
        get { return _player1Health; }
        set
        {
            if (_player1Health != value)
            {
                Player1HealthChanged(_player1Health, value);
                _player1Health = value;
            }
        }
    }
    public int Player1Wards
    {
        get { return _player1Wards; }
        set
        {
            if (_player1Wards != value)
            {
                Player1WardsChanged(_player1Wards, value);
                _player1Wards = value;
            }
        }
    }
    public int Player1MP
    {
        get { return _player1MP; }
        set
        {
            if (_player1MP != value)
            {
                Player1MPChanged(_player1MP, value);
                _player1MP = value;
            }
        }
    }
    public int Player2Health
    {
        get { return _player2Health; }
        set
        {
            if (_player2Health != value)
            {
                Player2HealthChanged(_player2Health, value);
                _player2Health = value;
            }
        }
    }
    public int Player2Wards
    {
        get { return _player2Wards; }
        set
        {
            if (_player2Wards != value)
            {
                Player2WardsChanged(_player2Wards, value);
                _player2Wards = value;
            }
        }
    }
    public int Player2MP
    {
        get { return _player2MP; }
        set
        {
            if (_player2MP != value)
            {
                Player2MPChanged(_player2MP, value);
                _player2MP = value;
            }
        }
    }

    // public events
    public delegate void DeathAction(int playerNumber);
    public event DeathAction OnDeathAction;

    public void Start()
    {
        Player1Health = maxHealth;
        Player1Wards = maxWards;
        Player1MP = 0;
        Player2Health = maxHealth;
        Player2Wards = maxWards;
        Player2MP = 0;
    }

    private void UpdateUIArray(GameObject[] arr, int value)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (i < value)
                arr[i].SetActive(true);
            else
                arr[i].SetActive(false);
        }
    }

    // change methods - update UI here

    private void Player1HealthChanged(int oldValue, int newValue)
    {
        UpdateUIArray(player1HeartIcons, newValue);
        if (newValue == 0)
            OnDeathAction(0);
    }

    private void Player1WardsChanged(int oldValue, int newValue)
    {
        UpdateUIArray(player1WardIcons, newValue);
    }

    private void Player1MPChanged(int oldValue, int newValue)
    {
        float normalizedMP = (float)newValue / maxMP;
        player1MPSlider.value = normalizedMP;
    }

    private void Player2HealthChanged(int oldValue, int newValue)
    {
        UpdateUIArray(player2HeartIcons, newValue);
        if (newValue == 0)
            OnDeathAction(1);
    }

    private void Player2WardsChanged(int oldValue, int newValue)
    {
        UpdateUIArray(player2WardIcons, newValue);
    }

    private void Player2MPChanged(int oldValue, int newValue)
    {
        float normalizedMP = (float)newValue / maxMP;
        player2MPSlider.value = normalizedMP;
    }
}
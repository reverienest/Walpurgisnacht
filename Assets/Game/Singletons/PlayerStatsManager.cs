using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsManager : Singleton<PlayerStatsManager>
{
    // public events
    public delegate void DeathAction(int playerNumber);
    public event DeathAction OnDeathAction;

    public delegate void HealthChangeAction(int playerNumber, int newHealthValue, int newWardValue);
    public event HealthChangeAction OnHealthChanged;

    public delegate void WardRefillAction(int playerNumber, int currentWardValue, float refillValue);
    public event WardRefillAction OnWardRefill;

    /*
    * UI components set by Unity
    */

    [SerializeField]
    private int maxHealth = 0;
    public int MaxHealth { get { return maxHealth; } }
    [SerializeField]
    private int maxMP = 0;
    public int MaxMP { get { return maxMP; } }

    [SerializeField]
    private Slider player1MPSlider = null;

    [SerializeField]
    private Slider player2MPSlider = null;

    // private backing variables

    private int _player1Health;
    private int _player1Wards;
    private float _player1WardRefill;
    private int _player1MP;

    private int _player2Health;
    private int _player2Wards;
    private float _player2WardRefill;
    private int _player2MP;

    // public properties

    public int Player1Health
    {
        get { return _player1Health; }
        set
        {
            if (value < 0)
            {
                value = 0;
            }

            if (value > maxHealth)
            {
                value = maxHealth;
            }

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
            if (value < 0)
            {
                value = 0;
            }

            if (value > maxHealth)
            {
                value = maxHealth;
            }

            if (_player1Wards != value)
            {
                Player1WardsChanged(_player1Wards, value);
                _player1Wards = value;
            }
        }
    }
    public float Player1WardRefill
    {
        get { return _player1WardRefill; }
        set
        {
            if (value < 0)
            {
                value = 0;
            }

            if (_player1WardRefill != value && _player1Wards <= maxHealth)
            {
                if (1 > value)
                {
                    OnWardRefill?.Invoke(0, Player1Wards, value);
                    _player1WardRefill = value;
                }
                else
                {
                    Player1Wards += 1;
                    _player1WardRefill = 0;
                }
            }
        }
    }
    public int Player1MP
    {
        get { return _player1MP; }
        set
        {
            if (value < 0)
            {
                value = 0;
            }

            if (value > maxMP)
            {
                value = maxMP;
            }

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
            if (value < 0)
            {
                value = 0;
            }

            if (value > maxHealth)
            {
                value = maxHealth;
            }

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
            if (value < 0)
            {
                value = 0;
            }

            if (value > maxHealth)
            {
                value = maxHealth;
            }

            if (_player2Wards != value)
            {
                Player2WardsChanged(_player2Wards, value);
                _player2Wards = value;
            }
        }
    }
    public float Player2WardRefill
    {
        get { return _player2WardRefill; }
        set
        {
            if (value < 0)
            {
                value = 0;
            }

            if (_player2WardRefill != value && _player2Wards <= maxHealth)
            {
                if (1 > value)
                {
                    OnWardRefill?.Invoke(1, Player2Wards, value);
                    _player2WardRefill = value;
                }
                else
                {
                    Player2Wards += 1;
                    _player2WardRefill = 0;
                }
            }
        }
    }
    public int Player2MP
    {
        get { return _player2MP; }
        set
        {
            if (value < 0)
            {
                value = 0;
            }

            if (value > maxMP)
            {
                value = maxMP;
            }

            if (_player2MP != value)
            {
                Player2MPChanged(_player2MP, value);
                _player2MP = value;
            }
        }
    }

    private int[] tempWards = new int[2];

    public void Start()
    {
        Player1Health = maxHealth;
        Player1Wards = maxHealth;
        Player1MP = 0;
        Player2Health = maxHealth;
        Player2Wards = maxHealth;
        Player2MP = 0;
    }

    /// Stores player's current ward count and removes them from actual ward count
    public void StashWards(int playerNumber)
    {
        if (playerNumber == 0)
        {
            tempWards[playerNumber] = Player1Wards;
            Player1Wards = 0;
        }
        else
        {
            tempWards[playerNumber] = Player2Wards;
            Player2Wards = 0;
        }
    }

    /// Restores player's old ward count
    public void PopWards(int playerNumber)
    {
        if (playerNumber == 0)
        {
            Player1Wards = Math.Min(tempWards[playerNumber], Player1Health);
        }
        else
        {
            Player2Wards = Math.Min(tempWards[playerNumber], Player2Health);
        }
    }

    // change methods - update UI here

    private void Player1HealthChanged(int oldValue, int newValue)
    {
        OnHealthChanged?.Invoke(0, newValue, Player1Wards);
        if (newValue == 0)
            OnDeathAction(0);
    }

    private void Player1WardsChanged(int oldValue, int newValue)
    {
        OnHealthChanged?.Invoke(0, Player1Health, newValue);
    }

    private void Player1MPChanged(int oldValue, int newValue)
    {
        float normalizedMP = (float)newValue / maxMP;
        player1MPSlider.value = normalizedMP;
    }

    private void Player2HealthChanged(int oldValue, int newValue)
    {
        OnHealthChanged?.Invoke(1, newValue, Player2Wards);
        if (newValue == 0)
            OnDeathAction(1);
    }

    private void Player2WardsChanged(int oldValue, int newValue)
    {
        OnHealthChanged?.Invoke(1, Player2Health, newValue);
    }

    private void Player2MPChanged(int oldValue, int newValue)
    {
        float normalizedMP = (float)newValue / maxMP;
        player2MPSlider.value = normalizedMP;
    }

    // Helper Methods

    public void TakeDamage(int playerNumber)
    {
        if (playerNumber == 0)
        {
            if (Player1Wards > 0)
                --Player1Wards;
            else
                --Player1Health;
        }
        else
        {
            if (Player2Wards > 0)
                --Player2Wards;
            else
                --Player2Health;
        }
    }

    public bool PlayerAtFullMP(int playerNumber)
    {
        if (0 == playerNumber)
        {
            return _player1MP == MaxMP;
        }
        else
        {
            return _player2MP == MaxMP;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISingleton : Singleton<UISingleton>
{
    protected UISingleton() { }

    public void Start()
    {
        Player1Ward = maxWard;
        Player1Health = maxHealth;
        Player1MagicalPower = 0;
        Player2Ward = maxWard;
        Player2Health = maxHealth;
        Player2MagicalPower = 0;
    }

    /*
    * UI components set by Unity
    */

    [SerializeField]
    private Image[] player1Hearts = null;

    [SerializeField]
    private Sprite fullHeart = null;

    [SerializeField]
    private Sprite emptyHeart = null;

    [SerializeField]
    private int maxHealth = 0;

    [SerializeField]
    private int maxWard = 0;

    /*
    * Player 1 Info
    */

    // private backing variables

    private int _player1Ward;
    private int _player1Health;
    private int _player1MagicalPower;

    // public variables

    public int Player1Ward
    {
        get { return _player1Ward; }
        set
        {
            if (_player1Ward != value)
            {
                Player1WardChanged(_player1Ward, value);
                _player1Ward = value;
            }
        }
    }

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

    public int Player1MagicalPower
    {
        get { return _player1MagicalPower; }
        set
        {
            if (_player1MagicalPower != value)
            {
                Player1MagicalPowerChanged(_player1MagicalPower, value);
                _player1MagicalPower = value;
            }
        }
    }

    // change methods - update UI here

    private void Player1WardChanged(int oldValue, int newValue)
    {

    }

    private void Player1HealthChanged(int oldValue, int newValue)
    {
        for (int i = 0; i < player1Hearts.Length; i++)
        {
            if (i < newValue)
            {
                player1Hearts[i].sprite = fullHeart;
            }
            else
            {
                player1Hearts[i].sprite = emptyHeart;
            }
        }
    }

    private void Player1MagicalPowerChanged(int oldValue, int newValue)
    {

    }


    /*
    * Player 2 Info
    */

    // private backing variables

    private int _player2Ward;
    private int _player2Health;
    private int _player2MagicalPower;

    // public variables

    public int Player2Ward
    {
        get { return _player2Ward; }
        set
        {
            if (_player2Ward != value)
            {
                Player2WardChanged(_player2Ward, value);
                _player2Ward = value;
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

    public int Player2MagicalPower
    {
        get { return _player2MagicalPower; }
        set
        {
            if (_player2MagicalPower != value)
            {
                Player2MagicalPowerChanged(_player2MagicalPower, value);
                _player2MagicalPower = value;
            }
        }
    }

    // change methods - update UI here

    private void Player2WardChanged(int oldValue, int newValue)
    {

    }

    private void Player2HealthChanged(int oldValue, int newValue)
    {

    }

    private void Player2MagicalPowerChanged(int oldValue, int newValue)
    {

    }


}
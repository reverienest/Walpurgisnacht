using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISingleton : Singleton<UISingleton>
{
    protected UISingleton() { }

    /*
    * Pass UI Components into the singleton here so they can be manipulated directly.
    */

    private Image[] Player1Hearts;
    private Sprite fullHeart;
    private Sprite emptyHeart;

    public void Setup(Image[] _Player1Hearts, Sprite _fullHeart, Sprite _emptyHeart)
    {
        Player1Hearts = _Player1Hearts;
        fullHeart = _fullHeart;
        emptyHeart = _emptyHeart;
    }


    /*
    * Player 1 Info
    */

    // private backing variables

    private int _Player1Ward = -1;
    private int _Player1Health = -1;
    private int _Player1MagicalPower = -1;

    // public variables

    public int Player1Ward
    {
        get { return _Player1Ward; }
        set
        {
            if (_Player1Ward != value)
            {
                _Player1WardChanged(_Player1Ward, value);
                _Player1Ward = value;
            }
        }
    }

    public int Player1Health
    {
        get { return _Player1Health; }
        set
        {
            if (_Player1Health != value)
            {
                _Player1HealthChanged(_Player1Health, value);
                _Player1Health = value;
            }
        }
    }

    public int Player1MagicalPower
    {
        get { return _Player1MagicalPower; }
        set
        {
            if (_Player1MagicalPower != value)
            {
                _Player1MagicalPowerChanged(_Player1MagicalPower, value);
                _Player1MagicalPower = value;
            }
        }
    }

    // change methods - update UI here

    private void _Player1WardChanged(int oldValue, int newValue)
    {

    }

    private void _Player1HealthChanged(int oldValue, int newValue)
    {
        for (int i = 0; i < Player1Hearts.Length; i++)
        {
            if (i < newValue)
            {
                Player1Hearts[i].sprite = fullHeart;
            }
            else
            {
                Player1Hearts[i].sprite = emptyHeart;
            }
        }
    }

    private void _Player1MagicalPowerChanged(int oldValue, int newValue)
    {

    }

    /*
    * Player 2 Info
    */

    // private backing variables

    private int _Player2Ward = -1;
    private int _Player2Health = -1;
    private int _Player2MagicalPower = -1;

    // public variables

    public int Player2Ward
    {
        get { return _Player2Ward; }
        set
        {
            if (_Player2Ward != value)
            {
                _Player2WardChanged(_Player2Ward, value);
                _Player2Ward = value;
            }
        }
    }

    public int Player2Health
    {
        get { return _Player2Health; }
        set
        {
            if (_Player2Health != value)
            {
                _Player2HealthChanged(_Player2Health, value);
                _Player2Health = value;
            }
        }
    }

    public int Player2MagicalPower
    {
        get { return _Player2MagicalPower; }
        set
        {
            if (_Player2MagicalPower != value)
            {
                _Player2MagicalPowerChanged(_Player2MagicalPower, value);
                _Player2MagicalPower = value;
            }
        }
    }

    // change methods - update UI here

    private void _Player2WardChanged(int oldValue, int newValue)
    {

    }

    private void _Player2HealthChanged(int oldValue, int newValue)
    {

    }

    private void _Player2MagicalPowerChanged(int oldValue, int newValue)
    {

    }


}
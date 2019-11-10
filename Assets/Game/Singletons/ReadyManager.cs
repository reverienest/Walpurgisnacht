using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReadyManager : Singleton<ReadyManager>
{
    public delegate void ReadyAction(int playerNumber);
    public event ReadyAction OnReady;
    public event ReadyAction OnNotReady;

    [SerializeField]
    private string mainSceneName = null;

    private bool[] playersReady = new bool[2];
    public bool this[int playerNumber]
    {
        get { return playersReady[playerNumber]; }
        set
        {
            if (value)
                OnReady?.Invoke(playerNumber);
            else
                OnNotReady?.Invoke(playerNumber);

            playersReady[playerNumber] = value;
            if (playersReady[0] && playersReady[1])
                SceneManager.LoadScene(mainSceneName);
        }
    }

}

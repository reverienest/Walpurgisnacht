using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action
{
    UP, DOWN, LEFT, RIGHT, PRIM, HEAVY, INTRIN, LAST_WORD, SLOW
}

public class InputMap : Singleton<InputMap>
{
    IDictionary<Action, KeyCode>[] actionMaps = {
        new Dictionary<Action, KeyCode>(), //Player 1
        new Dictionary<Action, KeyCode>(), //Player 2
    };

    void Awake()
    {
        actionMaps[0].Add(Action.UP, KeyCode.UpArrow);
        actionMaps[0].Add(Action.DOWN, KeyCode.DownArrow);
        actionMaps[0].Add(Action.LEFT, KeyCode.LeftArrow);
        actionMaps[0].Add(Action.RIGHT, KeyCode.RightArrow);
        actionMaps[0].Add(Action.PRIM, KeyCode.Z);
        actionMaps[0].Add(Action.HEAVY, KeyCode.X);
        actionMaps[0].Add(Action.INTRIN, KeyCode.C);
        actionMaps[0].Add(Action.LAST_WORD, KeyCode.A);
        actionMaps[0].Add(Action.SLOW, KeyCode.LeftShift);

        actionMaps[1].Add(Action.UP, KeyCode.I);
        actionMaps[1].Add(Action.DOWN, KeyCode.K);
        actionMaps[1].Add(Action.LEFT, KeyCode.J);
        actionMaps[1].Add(Action.RIGHT, KeyCode.L);
        actionMaps[1].Add(Action.PRIM, KeyCode.E);
        actionMaps[1].Add(Action.HEAVY, KeyCode.R);
        actionMaps[1].Add(Action.INTRIN, KeyCode.T);
        actionMaps[1].Add(Action.LAST_WORD, KeyCode.Alpha3);
        actionMaps[1].Add(Action.SLOW, KeyCode.RightShift);
    }

    public bool GetInput(int playerNumber, Action action)
    {
        return Input.GetKey(actionMaps[playerNumber][action]);
    }
    public bool GetInputUp(int playerNumber, Action action)
    {
        return Input.GetKeyUp(actionMaps[playerNumber][action]);
    }
    public bool GetInputDown(int playerNumber, Action action)
    {
        return Input.GetKeyDown(actionMaps[playerNumber][action]);
    }
    public void UpdateKeyCode(int playerNumber, Action action, KeyCode key)
    {
        actionMaps[playerNumber][action] = key;
    }
}

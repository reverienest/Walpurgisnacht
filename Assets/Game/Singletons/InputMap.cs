using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action
{
    UP, DOWN, LEFT, RIGHT, PRIM, HEAVY, INTRIN, LAST_WORD, SPELL_CARD
}

public class InputMap : Singleton<InputMap>
{
    IDictionary<Action, KeyCode> act = new Dictionary<Action, KeyCode>();

    void Start()
    {
        act.Add(Action.UP, KeyCode.UpArrow);
        act.Add(Action.DOWN, KeyCode.DownArrow);
        act.Add(Action.LEFT, KeyCode.LeftArrow);
        act.Add(Action.RIGHT, KeyCode.RightArrow);
        act.Add(Action.PRIM, KeyCode.Z);
        act.Add(Action.HEAVY, KeyCode.X);
        act.Add(Action.INTRIN, KeyCode.C);
        act.Add(Action.LAST_WORD, KeyCode.A);
        act.Add(Action.SPELL_CARD, KeyCode.D);
    }

    public bool GetInput(Action action)
    {
        return Input.GetKey(act[action]);
    }
    public bool GetInputUp(Action action)
    {
        return Input.GetKeyUp(act[action]);
    }
    public bool GetInputDown(Action action)
    {
        return Input.GetKeyDown(act[action]);
    }
    public void UpdateKeyCode(Action action, KeyCode key)
    {
        act[action] = key;
    }
}

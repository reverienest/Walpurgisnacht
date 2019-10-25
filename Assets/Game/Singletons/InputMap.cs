using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    UP, DOWN, LEFT, RIGHT, PRIM, HEAVY, INTRIN, MOVE, LAST_WORD, SLOW, CAST_CIRCLE, _NUM_TYPES
}

public class InputMap : Singleton<InputMap>
{
    public bool inputEnabled;

    IDictionary<ActionType, KeyCode>[] actionMaps = {
        new Dictionary<ActionType, KeyCode>(), //Player 1
        new Dictionary<ActionType, KeyCode>(), //Player 2
    };

    void Awake()
    {
        actionMaps[0].Add(ActionType.UP, KeyCode.UpArrow);
        actionMaps[0].Add(ActionType.DOWN, KeyCode.DownArrow);
        actionMaps[0].Add(ActionType.LEFT, KeyCode.LeftArrow);
        actionMaps[0].Add(ActionType.RIGHT, KeyCode.RightArrow);
        actionMaps[0].Add(ActionType.PRIM, KeyCode.Z);
        actionMaps[0].Add(ActionType.HEAVY, KeyCode.X);
        actionMaps[0].Add(ActionType.INTRIN, KeyCode.C);
        actionMaps[0].Add(ActionType.MOVE, KeyCode.V);
        actionMaps[0].Add(ActionType.LAST_WORD, KeyCode.A);
        actionMaps[0].Add(ActionType.SLOW, KeyCode.LeftShift);
        actionMaps[0].Add(ActionType.CAST_CIRCLE, KeyCode.LeftControl);

        actionMaps[1].Add(ActionType.UP, KeyCode.I);
        actionMaps[1].Add(ActionType.DOWN, KeyCode.K);
        actionMaps[1].Add(ActionType.LEFT, KeyCode.J);
        actionMaps[1].Add(ActionType.RIGHT, KeyCode.L);
        actionMaps[1].Add(ActionType.PRIM, KeyCode.E);
        actionMaps[1].Add(ActionType.HEAVY, KeyCode.R);
        actionMaps[1].Add(ActionType.INTRIN, KeyCode.T);
        actionMaps[1].Add(ActionType.MOVE, KeyCode.Y);
        actionMaps[1].Add(ActionType.LAST_WORD, KeyCode.Alpha3);
        actionMaps[1].Add(ActionType.SLOW, KeyCode.RightShift);
        actionMaps[1].Add(ActionType.CAST_CIRCLE, KeyCode.RightControl);
    }

    public bool GetInput(int playerNumber, ActionType actionType)
    {
        return inputEnabled && Input.GetKey(actionMaps[playerNumber][actionType]);
    }
    public bool GetInputUp(int playerNumber, ActionType actionType)
    {
        return inputEnabled && Input.GetKeyUp(actionMaps[playerNumber][actionType]);
    }
    public bool GetInputDown(int playerNumber, ActionType actionType)
    {
        return inputEnabled && Input.GetKeyDown(actionMaps[playerNumber][actionType]);
    }
    public void UpdateKeyCode(int playerNumber, ActionType actionType, KeyCode key)
    {
        actionMaps[playerNumber][actionType] = key;
    }
}

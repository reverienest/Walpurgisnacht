using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action
{
    UP, DOWN, LEFT, RIGHT, PRIM, HEAVY, INTRIN, LAST_WORD, SPELL_CARD
}

public class InputMap : MonoBehaviour
{
    IDictionary<Action, KeyCode> act = new Dictionary<Action, KeyCode>();
    // Start is called before the first frame update
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
    bool Input(Action action)
    {
        return InputMap.GetKey(act[action]);
    }
    void Update(Action action, KeyCode key)
    {
        act.Add(action, key);
    }
    bool InputUp(Action action)
    {
        return InputMap.GetKeyUp(act[action]);
    }
    bool InputDown(Action action)
    {
        return InputMap.GetKeyDown(act[action]);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action
{
    //PUT THIS SHIT IN ALL CAPS
    Up, Down, Left, Right, Fire1, Fire2, Fire3, shinMalphur, spellCard
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
    bool GetInput(Action action)
    {
        return Input.GetKey(act[action]);
    }
    void UpdateKeyCode(Action action, KeyCode key)
    {
        act.Add(action, key);
    }
    bool GetInputUp(Action action)
    {
        return Input.GetKeyUp(act[action]);
    }
    bool GetInputDown(Action action)
    {
        return Input.GetKeyDown(act[action]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

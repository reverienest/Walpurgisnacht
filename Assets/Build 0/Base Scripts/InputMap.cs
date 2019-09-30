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
        act.Add(Action.Up, KeyCode.W);
        act.Add(Action.Down, KeyCode.S);
        act.Add(Action.Left, KeyCode.A);
        act.Add(Action.Right, KeyCode.D);
        act.Add(Action.Fire1, KeyCode.)

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigator : MonoBehaviour
{
    [SerializeField][Tooltip("Set this to -1 for shared navigation")]
    private int playerNumber = -1;

    [SerializeField]
    private MenuNode start = null;

    private MenuNode _current;
    protected MenuNode Current
    {
        get { return _current; }
        set
        {
            if (_current != value)
            {
                _current?.DeselectNode();
                value.SelectNode();
            }
            _current = value;
        }
    }

    void Start()
    {
        Current = start;
        InputMap.Instance.inputEnabled = true;
    }

    void Update()
    {
        if (Current.Up && GetInputDown(ActionType.UP))
        {
            Current = Current.Up;
        }
        if (Current.Down && GetInputDown(ActionType.DOWN))
        {
            Current = Current.Down;
        }
        if (Current.Left && GetInputDown(ActionType.LEFT))
        {
            Current = Current.Left;
        }
        if (Current.Right && GetInputDown(ActionType.RIGHT))
        {
            Current = Current.Right;
        }
        if (GetInputDown(ActionType.CONFIRM))
        {
            Current.ActivateNode();
            if (Current.Forward)
                Current = Current.Forward;
        }
        if (GetInputDown(ActionType.BACK))
        {
            Current.DeactivateNode();
            if (Current.Backward)
                Current = Current.Backward;
        }
    }

    private bool GetInputDown(ActionType action)
    {
        if (playerNumber == -1)
            return InputMap.Instance.GetInputDown(0, action) || InputMap.Instance.GetInputDown(1, action);
        else
            return InputMap.Instance.GetInputDown(playerNumber, action);
    }
}

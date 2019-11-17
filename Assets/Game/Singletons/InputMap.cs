using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    CONFIRM, BACK, UP, DOWN, LEFT, RIGHT, PRIM, HEAVY, INTRIN, MOVE, LAST_WORD, SLOW, CAST_CIRCLE, _NUM_TYPES
}

public class InputMap : Singleton<InputMap>
{
    private struct ControllerInput
    {
        public string axisName;
        /// If 0, this input acts as a button, otherwise like an axis
        public float threshold;

        public ControllerInput(string axisName, float threshold = 0)
        {
            this.axisName = axisName;
            this.threshold = threshold;
        }

        public bool GetInput() { return threshold == 0 ? GetButton() : GetAxis(); }
        public bool GetInputDown() { return threshold == 0 ? GetButtonDown() : GetAxisDown(); }
        public bool GetInputUp() { return threshold == 0 ? GetButtonUp() : GetAxisUp(); }

        private bool GetAxis()
        {
            if (threshold > 0)
                return Input.GetAxisRaw(axisName) > threshold;
            else
                return Input.GetAxisRaw(axisName) < threshold;
        }
        private bool GetAxisUp()
        {
            float lastValue = InputMap.Instance.lastAxisValues[axisName];
            if (threshold < 0)
                return lastValue < threshold && Input.GetAxisRaw(axisName) > threshold;
            else
                return lastValue > threshold && Input.GetAxisRaw(axisName) < threshold;
        }
        private bool GetAxisDown()
        {
            float lastValue = InputMap.Instance.lastAxisValues[axisName];
            if (threshold > 0)
                return lastValue < threshold && Input.GetAxisRaw(axisName) > threshold;
            else
                return lastValue > threshold && Input.GetAxisRaw(axisName) < threshold;
        }

        private bool GetButton()
        {
            return Input.GetButton(axisName);
        }
        private bool GetButtonUp()
        {
            return Input.GetButtonUp(axisName);
        }
        private bool GetButtonDown()
        {
            return Input.GetButtonDown(axisName);
        }
    }

    public bool inputEnabled = true;

    [SerializeField]
    private float joystickThreshold = 0.2f;
    [SerializeField]
    private float triggerThreshold = 0.2f;

    private Dictionary<string, float> lastAxisValues = new Dictionary<string, float>()
    {
        { "P1Vertical", 0 },
        { "P2Vertical", 0 },
        { "P1Horizontal", 0 },
        { "P2Horizontal", 0 },
        { "P1LT", 0 },
        { "P2LT", 0 },
        { "P1RT", 0 },
        { "P2RT", 0 },
    };

    private Dictionary<ActionType, KeyCode>[] keyMaps = {
        new Dictionary<ActionType, KeyCode>(), // Player 1
        new Dictionary<ActionType, KeyCode>(), // Player 2
    };

    private Dictionary<ActionType, ControllerInput>[] controllerMaps = {
        new Dictionary<ActionType, ControllerInput>(), // Player 1
        new Dictionary<ActionType, ControllerInput>(), // Player 2
    };

    void Awake()
    {
        keyMaps[0].Add(ActionType.CONFIRM, KeyCode.Z);
        keyMaps[0].Add(ActionType.BACK, KeyCode.X);
        keyMaps[0].Add(ActionType.UP, KeyCode.UpArrow);
        keyMaps[0].Add(ActionType.DOWN, KeyCode.DownArrow);
        keyMaps[0].Add(ActionType.LEFT, KeyCode.LeftArrow);
        keyMaps[0].Add(ActionType.RIGHT, KeyCode.RightArrow);
        keyMaps[0].Add(ActionType.PRIM, KeyCode.Z);
        keyMaps[0].Add(ActionType.HEAVY, KeyCode.X);
        keyMaps[0].Add(ActionType.INTRIN, KeyCode.C);
        keyMaps[0].Add(ActionType.MOVE, KeyCode.V);
        keyMaps[0].Add(ActionType.LAST_WORD, KeyCode.A);
        keyMaps[0].Add(ActionType.SLOW, KeyCode.LeftShift);
        keyMaps[0].Add(ActionType.CAST_CIRCLE, KeyCode.LeftControl);

        controllerMaps[0].Add(ActionType.CONFIRM, new ControllerInput("P1A"));
        controllerMaps[0].Add(ActionType.BACK, new ControllerInput("P1B"));
        controllerMaps[0].Add(ActionType.UP, new ControllerInput("P1Vertical", -joystickThreshold));
        controllerMaps[0].Add(ActionType.DOWN, new ControllerInput("P1Vertical", joystickThreshold));
        controllerMaps[0].Add(ActionType.LEFT, new ControllerInput("P1Horizontal", -joystickThreshold));
        controllerMaps[0].Add(ActionType.RIGHT, new ControllerInput("P1Horizontal", joystickThreshold));
        controllerMaps[0].Add(ActionType.PRIM, new ControllerInput("P1RT", triggerThreshold));
        controllerMaps[0].Add(ActionType.HEAVY, new ControllerInput("P1LT", triggerThreshold));
        controllerMaps[0].Add(ActionType.INTRIN, new ControllerInput("P1B"));
        controllerMaps[0].Add(ActionType.MOVE, new ControllerInput("P1A"));
        controllerMaps[0].Add(ActionType.LAST_WORD, new ControllerInput("P1Y"));
        controllerMaps[0].Add(ActionType.SLOW, new ControllerInput("P1X"));
        controllerMaps[0].Add(ActionType.CAST_CIRCLE, new ControllerInput("P1Y"));

        keyMaps[1].Add(ActionType.CONFIRM, KeyCode.E);
        keyMaps[1].Add(ActionType.BACK, KeyCode.R);
        keyMaps[1].Add(ActionType.UP, KeyCode.I);
        keyMaps[1].Add(ActionType.DOWN, KeyCode.K);
        keyMaps[1].Add(ActionType.LEFT, KeyCode.J);
        keyMaps[1].Add(ActionType.RIGHT, KeyCode.L);
        keyMaps[1].Add(ActionType.PRIM, KeyCode.E);
        keyMaps[1].Add(ActionType.HEAVY, KeyCode.R);
        keyMaps[1].Add(ActionType.INTRIN, KeyCode.T);
        keyMaps[1].Add(ActionType.MOVE, KeyCode.Y);
        keyMaps[1].Add(ActionType.LAST_WORD, KeyCode.Alpha3);
        keyMaps[1].Add(ActionType.SLOW, KeyCode.RightShift);
        keyMaps[1].Add(ActionType.CAST_CIRCLE, KeyCode.RightControl);

        controllerMaps[1].Add(ActionType.CONFIRM, new ControllerInput("P2A"));
        controllerMaps[1].Add(ActionType.BACK, new ControllerInput("P2B"));
        controllerMaps[1].Add(ActionType.UP, new ControllerInput("P2Vertical", -joystickThreshold));
        controllerMaps[1].Add(ActionType.DOWN, new ControllerInput("P2Vertical", joystickThreshold));
        controllerMaps[1].Add(ActionType.LEFT, new ControllerInput("P2Horizontal", -joystickThreshold));
        controllerMaps[1].Add(ActionType.RIGHT, new ControllerInput("P2Horizontal", joystickThreshold));
        controllerMaps[1].Add(ActionType.PRIM, new ControllerInput("P2RT", triggerThreshold));
        controllerMaps[1].Add(ActionType.HEAVY, new ControllerInput("P2LT", triggerThreshold));
        controllerMaps[1].Add(ActionType.INTRIN, new ControllerInput("P2B"));
        controllerMaps[1].Add(ActionType.MOVE, new ControllerInput("P2A"));
        controllerMaps[1].Add(ActionType.LAST_WORD, new ControllerInput("P2Y"));
        controllerMaps[1].Add(ActionType.SLOW, new ControllerInput("P2X"));
        controllerMaps[1].Add(ActionType.CAST_CIRCLE, new ControllerInput("P2Y"));
    }

    void LateUpdate()
    {
        // Update lastAxisValues
        List<string> keys = new List<string>(lastAxisValues.Keys);
        foreach (string axisName in keys)
        {
            lastAxisValues[axisName] = Input.GetAxisRaw(axisName);
        }
    }

    public bool GetInput(int playerNumber, ActionType actionType)
    {
        return inputEnabled && (Input.GetKey(keyMaps[playerNumber][actionType])
            || controllerMaps[playerNumber][actionType].GetInput());
    }
    public bool GetInputUp(int playerNumber, ActionType actionType)
    {
        return inputEnabled && (Input.GetKeyUp(keyMaps[playerNumber][actionType])
            || controllerMaps[playerNumber][actionType].GetInputUp());
    }
    public bool GetInputDown(int playerNumber, ActionType actionType)
    {
        return inputEnabled && (Input.GetKeyDown(keyMaps[playerNumber][actionType])
            || controllerMaps[playerNumber][actionType].GetInputDown());
    }
    public void UpdateKeyCode(int playerNumber, ActionType actionType, KeyCode key)
    {
        keyMaps[playerNumber][actionType] = key;
    }
}

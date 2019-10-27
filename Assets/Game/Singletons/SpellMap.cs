using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public enum SpellType
{
    PRIM, HEAVY, INTRIN, MOVE, _NUM_TYPES
}

public class SpellMap : Singleton<SpellMap>
{
    [Serializable]
    private class Cooldowns
    {
        public float[] cooldowns = new float[4];
        public float this[int i] { get { return cooldowns[i]; } }
    }
    [SerializeField]
    private Cooldowns[] cooldowns = new Cooldowns[(int)CharacterType._NUM_TYPES];


    [SerializeField]
    private Slider[] player1SpellIcons = null;

    [SerializeField]
    private Slider[] player2SpellIcons = null;

    private float[][] cooldownTimers = new float[2][];

    private ActionType[] spellToActionMap = new ActionType[(int)SpellType._NUM_TYPES];

    void Start()
    {
        //Init cooldown timers
        cooldownTimers[0] = new float[(uint)SpellType._NUM_TYPES];
        cooldownTimers[1] = new float[(uint)SpellType._NUM_TYPES];

        //Init spell to action map
        spellToActionMap[(uint)SpellType.PRIM] = ActionType.PRIM;
        spellToActionMap[(uint)SpellType.HEAVY] = ActionType.HEAVY;
        spellToActionMap[(uint)SpellType.INTRIN] = ActionType.INTRIN;
        spellToActionMap[(uint)SpellType.MOVE] = ActionType.MOVE;
    }

    void Update()
    {
        //Update cooldown timers
        for (int playerNum = 0; playerNum < cooldownTimers.Length; ++playerNum)
            for (int spellType = 0; spellType < cooldownTimers[playerNum].Length; ++spellType)
                DecrementCooldown(Time.deltaTime, playerNum, (SpellType)spellType);
    }

    private bool SpellReady(int playerNumber, SpellType spellType) { return cooldownTimers[playerNumber][(int)spellType] <= 0.0f; }

    private void RestartCooldown(int playerNumber, SpellType spellType)
    {
        SetCooldown(GetInitialCooldown(playerNumber, spellType), playerNumber, spellType);
    }

    private void DecrementCooldown(float deltaTime, int playerNumber, SpellType spellType)
    {
        SetCooldown(Math.Max(0.0f, cooldownTimers[playerNumber][(int)spellType] - deltaTime), playerNumber, spellType);
    }

    private void SetCooldown(float newVal, int playerNumber, SpellType spellType)
    {
        if (cooldownTimers[playerNumber][(int)spellType] == newVal)
            return;

        cooldownTimers[playerNumber][(int)spellType] = newVal;
        float normalizedCooldown = (newVal / GetInitialCooldown(playerNumber, spellType));

        if (0 == playerNumber)
        {
            player1SpellIcons[(int)spellType].value = normalizedCooldown;
        }
        else
        {
            player2SpellIcons[(int)spellType].value = normalizedCooldown;
        }
    }

    private float GetInitialCooldown(int playerNumber, SpellType spellType)
    {
        CharacterType charType = MatchManager.Instance.GetPlayerCharacterType(playerNumber);
        return cooldowns[(int)charType][(int)spellType];
    }

    /// Upon returning true, this function will restart the cooldown on the given spell
    public bool GetSpell(int playerNumber, SpellType spellType)
    {
        if (!SpellReady(playerNumber, spellType))
            return false;

        ActionType actionType = spellToActionMap[(uint)spellType];
        if (!InputMap.Instance.GetInput(playerNumber, actionType))
            return false;

        RestartCooldown(playerNumber, spellType);
        return true;
    }

    /// Upon returning true, this function will restart the cooldown on the given spell
    public bool GetSpellDown(int playerNumber, SpellType spellType)
    {
        if (!SpellReady(playerNumber, spellType))
            return false;

        ActionType actionType = spellToActionMap[(uint)spellType];
        if (!InputMap.Instance.GetInputDown(playerNumber, actionType))
            return false;

        RestartCooldown(playerNumber, spellType);
        return true;
    }

    /// Upon returning true, this function will restart the cooldown on the given spell
    public bool GetSpellUp(int playerNumber, SpellType spellType)
    {
        if (!SpellReady(playerNumber, spellType))
            return false;

        ActionType actionType = spellToActionMap[(uint)spellType];
        if (!InputMap.Instance.GetInputUp(playerNumber, actionType))
            return false;

        RestartCooldown(playerNumber, spellType);
        return true;
    }
}

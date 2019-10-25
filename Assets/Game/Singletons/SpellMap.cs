using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum SpellType
{
    PRIM, HEAVY, INTRIN, MOVE, _NUM_TYPES
}

public class SpellMap : Singleton<SpellMap>
{
    [SerializeField]
    private float[] cooldowns = new float[4];

    private float[][] cooldownTimers = new float[2][];

    private ActionType[] spellToActionMap = new ActionType[(uint)SpellType._NUM_TYPES];

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
        for (uint i = 0; i < cooldownTimers.Length; ++i)
            for (uint j = 0; j < cooldownTimers[i].Length; ++j)
                cooldownTimers[i][j] -= Time.deltaTime;
    }

    private bool SpellReady(int playerNumber, SpellType spellType) { return cooldownTimers[playerNumber][(uint)spellType] <= 0.0f; }
    private void RestartCooldown(int playerNumber, SpellType spellType) { cooldownTimers[playerNumber][(uint)spellType] = cooldowns[(uint)spellType]; }

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

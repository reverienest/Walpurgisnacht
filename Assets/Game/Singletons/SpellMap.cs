using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public enum SpellType
{
    PRIM, HEAVY, INTRIN, MOVE, CIRCLE, _NUM_TYPES
}

public class SpellMap : Singleton<SpellMap>
{
    [Serializable]
    private class Cooldowns
    {
        public float[] cooldowns = new float[5];
        public float this[int i] { get { return cooldowns[i]; } }
    }
    [SerializeField]
    private Cooldowns[] cooldowns = new Cooldowns[(int)CharacterType._NUM_TYPES];


    [SerializeField]
    private Slider[] player1CooldownSliders = null;

    [SerializeField]
    private Slider[] player2CooldownSliders = null;

    [SerializeField]
    private GameObject[] magicCircleCooldownSlots= null;

    private float[][] cooldownTimers = new float[2][];

    private ActionType[] spellToActionMap = new ActionType[(int)SpellType._NUM_TYPES];

    private int lastWordCastBy = -1;

    void Awake()
    {
        //Init cooldown timers
        cooldownTimers[0] = new float[(uint)SpellType._NUM_TYPES];
        cooldownTimers[1] = new float[(uint)SpellType._NUM_TYPES];

        //Init spell to action map
        spellToActionMap[(uint)SpellType.PRIM] = ActionType.PRIM;
        spellToActionMap[(uint)SpellType.HEAVY] = ActionType.HEAVY;
        spellToActionMap[(uint)SpellType.INTRIN] = ActionType.INTRIN;
        spellToActionMap[(uint)SpellType.MOVE] = ActionType.MOVE;
        spellToActionMap[(uint)SpellType.CIRCLE] = ActionType.CAST_CIRCLE;

        MatchManager.Instance.OnLastWordStart += OnLastWordStart;
        MatchManager.Instance.OnLastWordEnd += OnLastWordEnd;
    }

    void Update()
    {
        //Update cooldown timers
        for (int playerNum = 0; playerNum < cooldownTimers.Length; ++playerNum)
            for (int spellType = 0; spellType < cooldownTimers[playerNum].Length; ++spellType)
                DecreaseCooldown(Time.deltaTime, playerNum, (SpellType)spellType);
    }

    public bool SpellReady(int playerNumber, SpellType spellType) { return cooldownTimers[playerNumber][(int)spellType] <= 0.0f; }

    private void RestartCooldown(int playerNumber, SpellType spellType)
    {
        SetCooldown(GetInitialCooldown(playerNumber, spellType), playerNumber, spellType);
    }

    private void DecreaseCooldown(float deltaTime, int playerNumber, SpellType spellType)
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
            player1CooldownSliders[(int)spellType].value = normalizedCooldown;
        }
        else
        {
            player2CooldownSliders[(int)spellType].value = normalizedCooldown;
        }
    }

    private float GetInitialCooldown(int playerNumber, SpellType spellType)
    {
        CharacterType charType = CharacterSelection.Instance.GetPlayerCharacterType(playerNumber);
        return cooldowns[(int)charType][(int)spellType];
    }

    public void ResetAllCooldowns()
    {
        for (int playerNum = 0; playerNum < cooldownTimers.Length; ++playerNum)
            for (int spellType = 0; spellType < cooldownTimers[playerNum].Length; ++spellType)
                SetCooldown(0, playerNum, (SpellType)spellType);
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

    public void HideCircleCooldown(int playerNumber)
    {
        magicCircleCooldownSlots[playerNumber].GetComponent<Animator>().Play("CooldownSlideOut");
    }
    
    public void ShowCircleCooldown(int playerNumber)
    {
        magicCircleCooldownSlots[playerNumber].GetComponent<Animator>().Play("CooldownSlideIn");
    }

    private void OnLastWordStart(int playerNum)
    {
        lastWordCastBy = playerNum;
    }


    private void OnLastWordEnd()
    {
        ShowCircleCooldown(lastWordCastBy);
        SetCooldown(GetInitialCooldown(lastWordCastBy, SpellType.CIRCLE), lastWordCastBy, SpellType.CIRCLE);
        lastWordCastBy = -1;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownSliders : MonoBehaviour
{
    [SerializeField]
    private int playerNumber = 0;

    [Serializable]
    private class CooldownIconsSprites
    {
        public Sprite[] sprites = new Sprite[(int)SpellType._NUM_TYPES];
    }
    [SerializeField]
    private CooldownIconsSprites[] cooldownIconSprites = new CooldownIconsSprites[(int)CharacterType._NUM_TYPES];

    [SerializeField]
    private Image[] cooldownIcons = new Image[(int)SpellType._NUM_TYPES];

    void Start()
    {
        CharacterType type = CharacterSelection.Instance.GetPlayerCharacterType(playerNumber);
        for (int spellType = 0; spellType < (int)SpellType._NUM_TYPES; ++spellType)
            cooldownIcons[spellType].sprite = cooldownIconSprites[(int)type].sprites[spellType];
    }
}

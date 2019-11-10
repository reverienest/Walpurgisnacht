using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSelectUIData", menuName = "ScriptableObjects/CharacterSelectUIData", order = 1)]
public class CharacterSelectUIData : ScriptableObject
{
    public string[] descriptionTexts = new string[(int)CharacterType._NUM_TYPES];
    public Sprite[] characterPortraits = new Sprite[(int)CharacterType._NUM_TYPES];
}

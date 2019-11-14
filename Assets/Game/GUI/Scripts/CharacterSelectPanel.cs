using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectPanel : MonoBehaviour
{
    [SerializeField]
    private int playerNumber = 0;

    [SerializeField]
    private CharacterSelectUIData characterSelectUIData = null;

    [SerializeField]
    private Text descriptionText = null;
    [SerializeField]
    private Image characterPortrait = null;
    [SerializeField]
    private MenuNode[] nodes = new MenuNode[(int)CharacterType._NUM_TYPES];

    [SerializeField]
    private GameObject confirmCursor = null;
    [SerializeField]
    private Color confirmColor = Color.black;

    void Start()
    {
        CharacterSelection.Instance.OnCharacterTypeChange += UpdateCharacterUI;
        ReadyManager.Instance.OnReady += ConfirmCharacter;
        ReadyManager.Instance.OnNotReady += UnconfirmCharacter;

        CharacterType type = CharacterSelection.Instance.GetPlayerCharacterType(playerNumber);
        UpdateCharacterUI(playerNumber, type);

        //Setup character button functions
        for (int i = 0; i < nodes.Length; ++i)
        {
            int iCopy = i;
            nodes[i].onSelect.AddListener(() =>
            {
                CharacterSelection.Instance.SetPlayerCharacterType(playerNumber, (CharacterType)iCopy);
            });
            nodes[i].onActivate.AddListener(() =>
            {
                ReadyManager.Instance[playerNumber] = true;
            });
            nodes[i].onDeactivate.AddListener(() =>
            {
                ReadyManager.Instance[playerNumber] = false;
            });
        }
    }

    private void UpdateCharacterUI(int playerNumber, CharacterType type)
    {
        if (playerNumber != this.playerNumber)
            return;
        descriptionText.text = characterSelectUIData.descriptionTexts[(int)type];
        characterPortrait.sprite = characterSelectUIData.characterPortraits[(int)type];
    }

    private void ConfirmCharacter(int playerNumber)
    {
        if (playerNumber != this.playerNumber)
            return;

        // A little hack part 1
        // Assuming node has an outline
        CharacterType type = CharacterSelection.Instance.GetPlayerCharacterType(playerNumber);
        nodes[(int)type].GetComponent<Outline>().effectColor = confirmColor;
    }

    private void UnconfirmCharacter(int playerNumber)
    {
        if (playerNumber != this.playerNumber)
            return;

        // A little hack part 2
        // Hardcoded setting back to white
        CharacterType type = CharacterSelection.Instance.GetPlayerCharacterType(playerNumber);
        nodes[(int)type].GetComponent<Outline>().effectColor = Color.white;
        confirmCursor.SetActive(false);
    }

    void OnDisable()
    {
        CharacterSelection.Instance.OnCharacterTypeChange -= UpdateCharacterUI;
    }
}

using UnityEngine;

public class CharacterSelection : Singleton<CharacterSelection>
{
    public delegate void CharacterTypeChangeAction(int playerNumber, CharacterType type);
    public event CharacterTypeChangeAction OnCharacterTypeChange;

    [SerializeField]
    private CharacterType[] playerCharacterTypes = new CharacterType[2]{CharacterType.SELENE, CharacterType.RHEA};
    public CharacterType GetPlayerCharacterType(int playerNumber) { return playerCharacterTypes[playerNumber]; }
    public void SetPlayerCharacterType(int playerNumber, CharacterType type)
    {
        playerCharacterTypes[playerNumber] = type;
        OnCharacterTypeChange?.Invoke(playerNumber, type);
    }

    void Start() { DontDestroyOnLoad(gameObject); }
}
public enum CharacterType {
    SELENE,
    RHEA,
    _NUM_TYPES
}

static class CharacterTypeMethods
{
    public static string GetString(this CharacterType characterType)
    {
        //Capitalize the first letter and make everything else lowercase
        string characterString = characterType.ToString();
        characterString = characterString.Substring(0, 1).ToUpper()
            + characterString.Substring(1).ToLower();
        return characterString;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastWordFooter : MonoBehaviour
{
    [SerializeField]
    private string[] lastWordNames = new string[2];
    [SerializeField]
    private Text titleText = null;
    [SerializeField]
    private Text timerText = null;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        MatchManager.Instance.OnLastWordTimerChange += OnLastWordTimerChange;
        MatchManager.Instance.OnLastWordStart += OnLastWordStart;
        MatchManager.Instance.OnLastWordEnd += OnLastWordEnd;
    }

    private void OnLastWordTimerChange(float oldValue, float newValue)
    {
        timerText.text = Mathf.CeilToInt(newValue).ToString();
    }

    private void OnLastWordStart(int playerNumber)
    {
        titleText.text = lastWordNames[(int)CharacterSelection.Instance.GetPlayerCharacterType(playerNumber)];
        anim.Play("LastWordIn");
        AudioManager.instance.Play("LastWordCasting");
    }

    private void OnLastWordEnd()
    {
        anim.Play("LastWordOut");
    }
}

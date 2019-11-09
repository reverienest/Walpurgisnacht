﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class VictoryOverlay : MonoBehaviour
{
    [SerializeField]
    private float maxBlurSize = 2;
    [SerializeField]
    private float transitionDuration = 2;
    [SerializeField]
    private Sprite[] characterVictorySprites = new Sprite[(int)CharacterType._NUM_TYPES];
    [SerializeField]
    private string titleMenuSceneName = null;

    [SerializeField]
    private Text[] playerScoreTexts = new Text[2];
    [SerializeField]
    private Text victorText = null;
    [SerializeField]
    private Image victorImage = null;

    private Animator anim;
    private CanvasGroup cg;
    private PostProcessVolume volume;
    private Blur blur;

    private float transitionTimer;
    private bool transitionDone;

    void Start()
    {
        anim = GetComponent<Animator>();
        cg = GetComponent<CanvasGroup>();
        blur = ScriptableObject.CreateInstance<Blur>();
        blur.enabled.Override(true);
        blur.BlurSize.Override(0);
        volume = PostProcessManager.instance.QuickVolume(0, 100, blur);

        MatchManager.Instance.OnVictoryAction += OnVictoryAction;
    }

    void Update()
    {
        if (transitionDone && Input.anyKeyDown)
        {
            SceneManager.LoadScene(titleMenuSceneName);
        }
    }

    private void OnVictoryAction(int playerNumber)
    {
        playerScoreTexts[0].text = MatchManager.Instance.GetPlayerScore(0).ToString();
        playerScoreTexts[1].text = MatchManager.Instance.GetPlayerScore(1).ToString();

        CharacterType victorCharacter = CharacterSelection.Instance.GetPlayerCharacterType(playerNumber);
        string victorCharacterString = victorCharacter.GetString();
        victorText.text = "(P" + (playerNumber + 1) + ") " + victorCharacterString;
        victorImage.sprite = characterVictorySprites[(int)victorCharacter];
        anim.Play("VictoryOverlay");
        StartCoroutine(BlurAndFade());
    }

    IEnumerator BlurAndFade()
    {
        for (transitionTimer = 0; transitionTimer < transitionDuration; transitionTimer += Time.deltaTime)
        {
            float normalized = transitionTimer / transitionDuration;
            blur.BlurSize.value = Mathf.Lerp(0, maxBlurSize, normalized);
            // cg.alpha = Mathf.Lerp(0, 1, normalized);
            yield return null;
        }
        transitionDone = true;
    }
}

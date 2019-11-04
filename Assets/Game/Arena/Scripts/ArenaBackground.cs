using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBackground : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        MatchManager.Instance.OnLastWordStart += OnLastWordStart;
        MatchManager.Instance.OnLastWordEnd += OnLastWordEnd;
    }

    private void OnLastWordStart(int playerNumber)
    {
        anim.Play("FadeOut");
    }

    private void OnLastWordEnd()
    {
        anim.Play("FadeIn");
    }
}

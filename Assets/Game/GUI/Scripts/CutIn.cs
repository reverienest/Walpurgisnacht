using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutIn : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        MatchManager.Instance.OnLastWordStart += OnLastWordStart;
    }

    private void OnLastWordStart(int playerNumber)
    {
        anim.Play("CutIn");
    }
}

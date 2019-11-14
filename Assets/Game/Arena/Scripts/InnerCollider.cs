using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerCollider : MonoBehaviour
{
    private EdgeCollider2D ec;

    void Start()
    {
        ec = GetComponent<EdgeCollider2D>();

        MatchManager.Instance.OnLastWordStart += OnLastWordStart;
        MatchManager.Instance.OnLastWordEnd += OnLastWordEnd;
    }

    private void OnLastWordStart(int playerNumber)
    {
        ec.enabled = false;
    }

    private void OnLastWordEnd()
    {
        ec.enabled = true;
    }
}

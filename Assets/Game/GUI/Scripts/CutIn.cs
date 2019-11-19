using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutIn : MonoBehaviour
{
    [SerializeField]
    private Sprite[] cutInSprites = new Sprite[2];

    private Animator anim;
    private Image image;

    void Start()
    {
        anim = GetComponent<Animator>();
        image = GetComponentInChildren<Image>();

        MatchManager.Instance.OnLastWordStart += OnLastWordStart;
    }

    private void OnLastWordStart(int playerNumber)
    {
        image.sprite = cutInSprites[playerNumber];
        if (playerNumber == 0)
        {
            anim.Play("CutInToRight", -1, 0);
        }
        else
        {
            anim.Play("CutInToLeft", -1, 0);
        }
    }
}

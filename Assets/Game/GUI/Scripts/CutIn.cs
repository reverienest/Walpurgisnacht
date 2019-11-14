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

    private Vector3 baseImageScale;

    void Start()
    {
        anim = GetComponent<Animator>();
        image = GetComponentInChildren<Image>();

        baseImageScale = transform.GetChild(0).localScale;

        MatchManager.Instance.OnLastWordStart += OnLastWordStart;
    }

    private void OnLastWordStart(int playerNumber)
    {
        image.sprite = cutInSprites[playerNumber];
        if (playerNumber == 0)
        {
            transform.localScale = baseImageScale;
            anim.Play("CutInToRight");
        }
        else
        {
            transform.localScale = new Vector3(-baseImageScale.x, baseImageScale.y, baseImageScale.z);
            anim.Play("CutInToLeft");
        }
    }
}

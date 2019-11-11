using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaLinker : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer linkedSpriteRenderer = null;

    private SpriteRenderer sr;

    private float alphaRatio;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        alphaRatio = sr.color.a / linkedSpriteRenderer.color.a;
    }

    void Update()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, linkedSpriteRenderer.color.a * alphaRatio);
    }
}

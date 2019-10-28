using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class SplashText : MonoBehaviour
{
    public delegate void EndAction();

    private EndAction callback;
    private string textString;

    private Animator anim;
    private Text text;
    private RectTransform rt;

    public void Init(string textString, EndAction callback)
    {
        this.textString = textString;
        this.callback = callback;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        text = GetComponent<Text>();
        rt = GetComponent<RectTransform>();

        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = Vector2.zero;

        text.text = textString;

        anim.Play("SplashText");
    }

    private void OnAnimationEnd()
    {
        callback?.Invoke();
        Destroy(gameObject);
    }
}

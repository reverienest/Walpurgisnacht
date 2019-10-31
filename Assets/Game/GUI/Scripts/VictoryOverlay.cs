using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class VictoryOverlay : MonoBehaviour
{
    [SerializeField]
    private float maxBlurSize = 2;
    [SerializeField]
    private float transitionDuration = 2;

    [SerializeField]
    private Text[] playerScoreTexts = new Text[2];

    private CanvasGroup cg;
    private PostProcessVolume volume;
    private Blur blur;

    private float transitionTimer;

    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        blur = ScriptableObject.CreateInstance<Blur>();
        blur.enabled.Override(true);
        blur.BlurSize.Override(0);
        volume = PostProcessManager.instance.QuickVolume(0, 100, blur);

        MatchManager.Instance.OnVictoryAction += OnVictoryAction;
    }

    private void OnVictoryAction()
    {
        StartCoroutine(AnimateBlur());
    }

    IEnumerator AnimateBlur()
    {
        for (transitionTimer = 0; transitionTimer < transitionDuration; transitionTimer += Time.deltaTime)
        {
            float normalized = transitionTimer / transitionDuration;
            blur.BlurSize.value = Mathf.Lerp(0, maxBlurSize, normalized);
            cg.alpha = Mathf.Lerp(0, 1, normalized);
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Fade : MonoBehaviour
{

    enum Mode
    {
        FadeIn,
        FadeOut,
    }

    [SerializeField, Header("フェード時間")]
    private float fadeTime;
    [SerializeField, Header("フェードの種類")]
    private Mode mode;

    private bool bFade;
    private float fadeCount;
    private Image image;
    private UnityEvent onFadeComplete = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {

        image = GetComponent<Image>();
        switch (mode)
        {
            case Mode.FadeIn: fadeCount = fadeTime; break;
            case Mode.FadeOut: fadeCount = 0; break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        FadeMode();
    }

    private void FadeMode()
    {
        if (!bFade) return;

        switch (mode)
        {
            case Mode.FadeIn: FadeIn(); break;
            case Mode.FadeOut: FadeOut(); break;
        }
        float alpha = fadeCount / fadeTime;
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);

    }

    private void FadeIn()
    {
        fadeCount -= Time.deltaTime;
        if (fadeCount <= 0)
        {
            mode = Mode.FadeOut;
            bFade = false;
            onFadeComplete.Invoke();
        }
    }

    private void FadeOut()
    {
        fadeCount += Time.deltaTime;
        if (fadeCount >= fadeTime)
        {
            mode = Mode.FadeIn;
            bFade = false;
            onFadeComplete.Invoke();
        }
    }

    public void FadeStart(UnityAction listener)
    {
        if (bFade) return;
        bFade = true;
        onFadeComplete.AddListener(listener);
    }
}

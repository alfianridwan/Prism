using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageCycle : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite[] sprites;
    public float fadeTime = 1f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(CycleSprites());
    }

    IEnumerator CycleSprites()
    {
        foreach (Sprite sprite in sprites)
        {
            sr.sprite = sprite;

            LeanTween.alpha(gameObject, 1f, fadeTime).setEase(LeanTweenType.easeInOutSine);

            yield return new WaitForSeconds(fadeTime);

            LeanTween.alpha(gameObject, 0f, fadeTime).setEase(LeanTweenType.easeInOutSine);

            yield return new WaitForSeconds(fadeTime);
        }

        StartCoroutine(CycleSprites());
    }
}

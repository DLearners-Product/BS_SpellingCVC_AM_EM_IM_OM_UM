using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class T3AnimationFrame2 : MonoBehaviour
{
     public GameObject mainImage;

    public GameObject vowelToActivate;

    public AudioSource source;

    public GameObject vowelScaleUp;

    public CanvasGroup canvasGroup; // Add this for CanvasGroup fade

    private void Start()
    {
        // Set initial values
        vowelScaleUp.transform.localScale = Vector3.one; // Start at scale (1,1,1)
        canvasGroup.alpha = 0; // Start CanvasGroup invisible

        // Start the animation
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        // Scale up the vowel and fade in the CanvasGroup at the same time
        ScaleAndFadeVowel();
        yield return new WaitForSeconds(1.3f); // Wait for the scaling and fading animation

        source.Play();
        yield return new WaitForSeconds(1f);

        mainImage.SetActive(false);
        vowelToActivate.SetActive(true);
    }

    void ScaleAndFadeVowel()
    {
        // Store the initial scale of the object
        Vector3 initialScale = vowelScaleUp.transform.localScale;

        // Scale up to (1.2, 1.2) and then scale back to the initial scale with a fancy easing effect
        vowelScaleUp.transform.DOScale(new Vector3(1.2f, 1.2f, 1), 1f)
            .SetEase(Ease.OutBounce) // Fancy ease effect for scale up
            .OnComplete(() =>
            {
                // Scale back to the initial scale
                vowelScaleUp.transform.DOScale(initialScale, 1f)
                    .SetEase(Ease.InOutSine); // Smooth transition back
            });

        // Simultaneously fade in the CanvasGroup
        canvasGroup.DOFade(1, 1f) // Fade in over 1 second
            .SetEase(Ease.Linear); // Smooth fade effect
    }
}

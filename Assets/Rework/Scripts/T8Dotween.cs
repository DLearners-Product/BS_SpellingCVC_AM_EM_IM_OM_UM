using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class T8Dotween : MonoBehaviour
{
    [Header("Objects to Activate")]
    public GameObject[] objects; // Assign objects in Inspector
    public float activationDelay = 0.2f; // Delay between each object's effect
    public float scaleDuration = 0.5f; // Duration for scaling effect
    public float fadeDuration = 0.5f; // Duration for fading effect

    private void Start()
    {
        StartCoroutine(ActivateObjectsWithEffects());
    }

    private IEnumerator ActivateObjectsWithEffects()
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(true);

                // Ensure object has an Image or SpriteRenderer for fade effect
                Image img = obj.GetComponent<Image>();
                SpriteRenderer sprite = obj.GetComponent<SpriteRenderer>();

                if (img != null) img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
                if (sprite != null) sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);

                // Scale from zero with bounce effect
                obj.transform.localScale = Vector3.zero;
                obj.transform.DOScale(Vector3.one, scaleDuration).SetEase(Ease.OutBack);

                // Fade in
                if (img != null) img.DOFade(1, fadeDuration);
                if (sprite != null) sprite.DOFade(1, fadeDuration);

                yield return new WaitForSeconds(activationDelay); // Wait before activating the next object
            }
        }
    }

}

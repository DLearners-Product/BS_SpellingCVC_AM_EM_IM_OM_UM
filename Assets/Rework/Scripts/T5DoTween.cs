using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
public class T5DoTween : MonoBehaviour
{
    public GameObject[] objects;
    private float scaleDuration = 0.2f;
    private float activationInterval = 0.3f;

    private void Start()
    {
        // Set initial scale of all objects to zero
        foreach (GameObject obj in objects)
        {
            obj.transform.localScale = Vector3.zero;
        }

        StartCoroutine(ActivateObjects());
    }

    private IEnumerator ActivateObjects()
    {
        // Scale up the first object with OutBounce easing, no punch scale
        if (objects.Length > 0)
        {
            GameObject firstObj = objects[0];
            firstObj.transform.localScale = Vector3.zero; // Ensure it starts at scale zero
            firstObj.GetComponent<CanvasGroup>().alpha = 0; // Make sure it starts invisible

            // Play audio if the object has an AudioSource component
            AudioSource firstAudio = firstObj.GetComponent<AudioSource>();
            if (firstAudio != null)
            {
                firstAudio.Play();
            }

            // Combine scaling, rotation, and fade-in for the first object
            firstObj.transform.DOScale(Vector3.one, scaleDuration)
                .SetEase(Ease.OutBounce); // OutBounce for the first object scaling
            firstObj.transform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360); // Spin while scaling
            firstObj.GetComponent<CanvasGroup>().DOFade(1f, 0.5f); // Fade in

            yield return new WaitForSeconds(activationInterval);
        }

        // Activate the remaining objects sequentially with punch scaling, rotation, and fade-in
        for (int i = 1; i < objects.Length; i++)
        {
            GameObject obj = objects[i];

            obj.transform.localScale = Vector3.zero; // Ensure it starts at scale zero
            obj.GetComponent<CanvasGroup>().alpha = 0; // Make sure it starts invisible

            // Play audio if the object has an AudioSource component
            AudioSource audio = obj.GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.Play();
            }

            // Combine scaling, rotation, and punch scale
            obj.transform.DOScale(Vector3.one, scaleDuration)
                .OnComplete(() => obj.transform.DOPunchScale(Vector3.one * 0.2f, 0.5f, 5, 0.5f)); // Punch scale after scaling
            obj.transform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360); // Spin while scaling
            obj.GetComponent<CanvasGroup>().DOFade(1f, 0.5f); // Fade in

            yield return new WaitForSeconds(activationInterval); // Wait before activating the next object
        }
    }

    public void DeactivateObjects()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.2f);
        foreach (var obj in objects)
        {
            // Punch scale for a quick bounce effect before shrinking
            obj.transform.DOPunchScale(Vector3.one * 0.1f, 0.3f, 5, 1)
                .OnComplete(() =>
                {
                    // After punch, scale down to zero with elastic effect
                    obj.transform.DOScale(Vector3.zero, 0.7f)
                        .SetEase(Ease.InElastic); // Stretchy shrink
                });
        }
    }
}

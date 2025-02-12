using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // DOTween namespace

public class T8SpriteChange : MonoBehaviour
{
   [SerializeField] private Image targetImage; // The UI Image component to change
    [SerializeField] private float transitionDuration = 0.5f; // Duration of effects

    private void Start()
    {
        if (targetImage == null)
            targetImage = GetComponent<Image>();
    }

    // 1. Flash & Scale Effect
    public void ChangeImageWithFlash(Sprite newSprite)
    {
        targetImage.DOColor(Color.white, 0.1f).OnComplete(() =>
        {
            targetImage.transform.DOScale(1.2f, 0.2f).OnComplete(() =>
            {
                targetImage.sprite = newSprite;
                targetImage.DOColor(Color.white, 0.1f);
                targetImage.transform.DOScale(1f, 0.2f);
            });
        });
    }

    // 2. Card Flip Effect
    public void ChangeImageWithFlip(Sprite newSprite)
    {
        targetImage.transform.DORotate(new Vector3(0, 90, 0), transitionDuration / 2).OnComplete(() =>
        {
            targetImage.sprite = newSprite;
            targetImage.transform.DORotate(Vector3.zero, transitionDuration / 2);
        });
    }

    // 3. Glitch Shake Effect
    public void ChangeImageWithGlitch(Sprite newSprite)
    {
        targetImage.transform.DOShakePosition(0.3f, 10, 20, 90).OnComplete(() =>
        {
            targetImage.sprite = newSprite;
        });
    }

    // 4. Spiral Shrink Effect
    public void ChangeImageWithSpiral(Sprite newSprite)
    {
        targetImage.transform.DOScale(Vector3.zero, transitionDuration / 2).SetEase(Ease.InBack).OnComplete(() =>
        {
            targetImage.sprite = newSprite;
            targetImage.transform.DOScale(Vector3.one, transitionDuration / 2).SetEase(Ease.OutBack);
        });
    }

    // 5. Rainbow Color Flash Effect
    public void ChangeImageWithRainbow(Sprite newSprite)
    {
        targetImage.DOColor(Color.red, 0.1f).OnComplete(() =>
        {
            targetImage.DOColor(Color.blue, 0.1f).OnComplete(() =>
            {
                targetImage.DOColor(Color.green, 0.1f).OnComplete(() =>
                {
                    targetImage.sprite = newSprite;
                    targetImage.DOColor(Color.white, 0.1f);
                });
            });
        });
    }

    // Call this method to trigger any effect
    public void ChangeImage(Sprite newSprite, int effectType)
    {
        switch (effectType)
        {
            case 1:
                ChangeImageWithFlash(newSprite);
                break;
            case 2:
                ChangeImageWithFlip(newSprite);
                break;
            case 3:
                ChangeImageWithGlitch(newSprite);
                break;
            case 4:
                ChangeImageWithSpiral(newSprite);
                break;
            case 5:
                ChangeImageWithRainbow(newSprite);
                break;
            default:
                Debug.LogWarning("Invalid effect type! Choose 1-5.");
                break;
        }
    }
}

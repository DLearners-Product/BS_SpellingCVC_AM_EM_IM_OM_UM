using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class T3PopUpAnimation : MonoBehaviour
{
     public List<GameObject> items = new List<GameObject>();
    public float fadeTime= 1f;

   
    void Start()
    {
        StartCoroutine(ItemsAnimation());
    }

    IEnumerator ItemsAnimation()
    {
        foreach(var item in items)
        {
            item.transform.localScale = Vector3.zero;
        }
         foreach(var item in items)
        {
            item.transform.DOScale(1f,fadeTime).SetEase(Ease.OutBounce);
           
            yield return new WaitForSeconds(0.25f);
        }
    }
}

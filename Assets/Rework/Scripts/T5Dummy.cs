using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T5Dummy : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrollgame : MonoBehaviour
{
    public int I_objectNumber;
    public Sprite[] SPRA_objects;
    public AudioClip[] ACA_objects;
    public GameObject[] GA_words;
    public GameObject G_object;
    public AnimationClip AC_scroll;
    public Animator AN_scroll;
    public Button BUTTON_scroll;

    void Start()
    {
        I_objectNumber = -1;
        G_object.SetActive(false);
    }

  
   public void BUT_clickScroll()
    {
        G_object.SetActive(false);
        for (int i = 0; i < GA_words.Length; i++)
        {
            GA_words[i].SetActive(false);
        }
        I_objectNumber++;
        AN_scroll.Play("afterclickani");
        BUTTON_scroll.interactable = false;
        Invoke("THI_showObject", AC_scroll.length);
        if(I_objectNumber==9)
        {
            BUTTON_scroll.gameObject.SetActive(false);
        }
    }

    public void THI_showObject()
    {

        G_object.SetActive(true);
        BUTTON_scroll.interactable = true;
        G_object.GetComponent<Image>().sprite = SPRA_objects[I_objectNumber];
        G_object.GetComponent<AudioSource>().clip = ACA_objects[I_objectNumber];
        for(int i = 0; i <GA_words.Length;i++)
        {
            GA_words[i].SetActive(false);
        }
        GA_words[I_objectNumber].SetActive(true);
        AN_scroll.Play("default");
    }
}

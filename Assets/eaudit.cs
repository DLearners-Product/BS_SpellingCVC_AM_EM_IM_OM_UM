using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eaudit : MonoBehaviour
{
    
    public int I_count;
    public AudioSource[] AS_words;
    public static eaudit OBJ_eaudit;

    public void Start()
    {
        OBJ_eaudit = this;
        I_count = 0;
        Invoke("THI_Sound", 1f);
    }


    public void BUT_next()
    {
        I_count++;
        THI_Sound();
    }

    public void THI_Sound()
    {
        AS_words[I_count].Play();
    }


}

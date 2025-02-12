using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class reviewahand : MonoBehaviour
{
   
    public int I_obj;


    public GameObject[] GA_objs;


    public  void BUT_clickObj()
    {
        for(int i = 0; i <GA_objs.Length;i++)
        {
            GA_objs[i].GetComponent<Button>().enabled = false;
        }
        Invoke("THI_enableButton", 2f);
    }

  public void THI_enableButton()
    {
        for (int i = 0; i < GA_objs.Length; i++)
        {
            GA_objs[i].GetComponent<Button>().enabled = true;
        }
    }
    
}

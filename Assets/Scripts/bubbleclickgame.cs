using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class bubbleclickgame : MonoBehaviour
{


    public GameObject G_selection;
    public int I_answerNumber;
    public GameObject[] GA_answers;
    public AudioClip[] ACA_clips;
    public AudioSource AS_right, AS_wrong;


    public void BUT_forward()
    {
        if (I_answerNumber < GA_answers.Length-1)
        {
            I_answerNumber++;
            for (int i = 0; i < GA_answers.Length; i++)
            {
                GA_answers[i].SetActive(false);
            }
            GA_answers[I_answerNumber].SetActive(true);
        }
    }
    public void BUT_Backward()
    {
        if (I_answerNumber > 0)
        {
            I_answerNumber--;
            for (int i = 0; i < GA_answers.Length; i++)
            {
                GA_answers[i].SetActive(false);
            }
            GA_answers[I_answerNumber].SetActive(true);
        }
    }
    public void BUT_Answer()
    {
        G_selection = EventSystem.current.currentSelectedGameObject;
        if(GA_answers[I_answerNumber].name==G_selection.name)
        {
            AS_right.clip = ACA_clips[I_answerNumber];
            AS_right.Play();
            GA_answers[I_answerNumber].GetComponent<Image>().material = null;
            G_selection = null;
        }
        else
        {
            AS_wrong.Play();
            G_selection = null;
        }
    }
}

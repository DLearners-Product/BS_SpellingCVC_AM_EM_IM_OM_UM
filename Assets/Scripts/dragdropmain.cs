using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragdropmain : MonoBehaviour
{
    public string GameName;
    public static dragdropmain OBJ_ddmain;
    public Sprite SPR_default, SPR_wrong, SPR_right;
    public AudioSource AS_wrong, AS_correct;
    public int I_question;
    public int I_matchCount;
    public GameObject[] GA_questions;
    public GameObject G_levelcomp;

    void Start()
    {
        OBJ_ddmain = this;
        I_question = -1;
        THI_showQuestino();
        G_levelcomp.SetActive(false);
    }

    public void THI_showQuestino()
    {
        I_question++;
        if (I_question < GA_questions.Length)
        {
            for (int i = 0; i < GA_questions.Length; i++)
            {
                GA_questions[i].SetActive(false);
            }
            GA_questions[I_question].SetActive(true);
            I_matchCount = 0;
        }
        else
        {
            //levelcomp!!
            G_levelcomp.SetActive(true);
        }
    }

   public void THI_delayQuestion()
    {
        Invoke("THI_showQuestino", 1f);
    }
}

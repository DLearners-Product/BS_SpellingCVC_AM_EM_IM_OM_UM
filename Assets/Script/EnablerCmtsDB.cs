using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class EnablerCmtsDB
{
    public string welcome;
    public string slide2;
    public string slide3;
    public string slide4;
    public string slide5;
    public string slide6;
    public string slide7;
    public string slide8;

    public string goodbye;

    public EnablerCmtsDB()
    {
        welcome = Main_Blended.OBJ_main_blended.enablerComments[0];
        slide2 = Main_Blended.OBJ_main_blended.enablerComments[1];
        slide3 = Main_Blended.OBJ_main_blended.enablerComments[2];
        slide4 = Main_Blended.OBJ_main_blended.enablerComments[3];
        slide5 = Main_Blended.OBJ_main_blended.enablerComments[4];
        slide6 = Main_Blended.OBJ_main_blended.enablerComments[5];
        slide7 = Main_Blended.OBJ_main_blended.enablerComments[6];
        slide8 = Main_Blended.OBJ_main_blended.enablerComments[7];
       
        goodbye = Main_Blended.OBJ_main_blended.enablerComments[8];
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveFalseManager : MonoBehaviour
{
  public GameObject[] activeFalse;


 void Start()
 {
    foreach(var ActiveFalse in activeFalse)
    {
        ActiveFalse.SetActive(false);
    }
 }
}

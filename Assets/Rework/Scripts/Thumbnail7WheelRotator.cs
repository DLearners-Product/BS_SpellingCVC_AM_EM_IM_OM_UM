using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thumbnail7WheelRotator : MonoBehaviour
{

 private float rotZ;
 public float rotaionSpeed; 
 
 public bool clockWiseRotation;

    // Update is called once per frame
    void Update()
    {
        if(clockWiseRotation == true)
        {
            rotZ += Time.deltaTime * rotaionSpeed;
        }

        else
        {
            rotZ += -Time.deltaTime * rotaionSpeed;
        }

        transform.rotation = Quaternion.Euler(0,0,rotZ);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class spinWheel : MonoBehaviour
{
    public GameObject wheel;
    public AudioSource audioSource;
    public AudioClip[] audios;

    int count;

    public void Spin()
    {
        if (count == 5)
        {
            Debug.Log("Game Over");
            count = 0;
        }
        switch (count)
        {
            case 0:
                wheel.GetComponent<Animator>().Play("wheel_anim_1");
                audioSource.clip = audios[0];
                Invoke("THI_playAudio", 1f);
                break;

            case 1:
                wheel.GetComponent<Animator>().Play("wheel_anim_2");
                audioSource.clip = audios[1];
                Invoke("THI_playAudio", 1f);
                break;

            case 2:
                wheel.GetComponent<Animator>().Play("wheel_anim_3");
                audioSource.clip = audios[2];
                Invoke("THI_playAudio", 1f);
                break;

            case 3:
                wheel.GetComponent<Animator>().Play("wheel_anim_4");
                audioSource.clip = audios[3];
                Invoke("THI_playAudio", 1f);
                break;

            case 4:
                wheel.GetComponent<Animator>().Play("wheel_anim_5");
                audioSource.clip = audios[4];
                Invoke("THI_playAudio", 1f);
                break;
        }
        count++;

    }
    public void THI_playAudio()
    {
        audioSource.Play();
    }
}
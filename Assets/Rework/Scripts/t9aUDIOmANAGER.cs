using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class t9aUDIOmANAGER : MonoBehaviour
{
     public AudioClip[] clips;  // Assign all available audio clips in Inspector
    public AudioSource source; // The shared AudioSource for playback

    public Sprite pauseSprite;  // Sprite for pause
    public Sprite playSprite;   // Sprite for play

    private int currentIndex = -1;  // Track the currently playing audio index
    private Button currentButton = null; // Track the currently playing button

    // Function to be called from buttons
    public void PlayAudio(int index)
    {
        Button clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        // If the same audio is playing, toggle play/pause
        if (currentIndex == index && source.isPlaying)
        {
            PauseAudio();
            return;
        }

        // If another clip is playing, stop it first
        if (source.isPlaying)
        {
            ResetAudio();
        }

        // Play the new clip
        if (index >= 0 && index < clips.Length)
        {
            source.clip = clips[index];
            source.Play();

            // Update state
            currentIndex = index;
            currentButton = clickedButton;

            // Update sprite to pause
            currentButton.image.sprite = pauseSprite;

            // Start monitoring audio completion
            StartCoroutine(WaitForAudioToEnd(index));
        }
    }

    // Coroutine to monitor when the audio finishes
    private IEnumerator WaitForAudioToEnd(int index)
    {
        yield return new WaitUntil(() => !source.isPlaying);

        // Only reset if the same audio was playing (to avoid conflicts if a new clip started)
        if (currentIndex == index)
        {
            ResetAudio();
        }
    }

    // Pause the current audio
    public void PauseAudio()
    {
        source.Pause();
        if (currentButton != null)
        {
            currentButton.image.sprite = playSprite;
        }
    }

    // Stop/reset the previous audio
    public void ResetAudio()
    {
        if (source.isPlaying)
        {
            source.Stop();
        }

        if (currentButton != null)
        {
            currentButton.image.sprite = playSprite;
        }

        currentIndex = -1;
        currentButton = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotManager : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        T9DraggableItem draggableItem = dropped.GetComponent<T9DraggableItem>();
        draggableItem.parentAfterDrag = transform;
        Debug.Log(this.gameObject.name);
        AudioSource audioSource = this.gameObject.GetComponent<AudioSource>();

        // Play the audio if the AudioSource component is attached
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

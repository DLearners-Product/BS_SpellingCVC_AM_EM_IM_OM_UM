using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class T3Frame1 : MonoBehaviour
{
     public GameObject[] objects;

    // Array of GameObjects to activate for each corresponding object
    public GameObject[] objectsToActivate;

    void Start()
    {
        // Check if the arrays are of the same length
        if (objects.Length != objectsToActivate.Length)
        {
            Debug.LogError("The number of objects and objects to activate must be the same!");
            return;
        }

        // Assign the button click event for each object
        for (int i = 0; i < objects.Length; i++)
        {
            int index = i; // Capture the index to avoid closure issue
            Button button = objects[i].GetComponent<Button>();

            if (button != null)
            {
                button.onClick.AddListener(() => OnObjectClicked(index));
            }
            else
            {
                Debug.LogError("No Button component found on: " + objects[i].name);
            }
        }
    }

    // Method to handle the object click by index
    void OnObjectClicked(int index)
    {
        // Deactivate all objects first (optional)
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(false);
        }

        // Activate the corresponding GameObject
        if (index >= 0 && index < objectsToActivate.Length)
        {
            objectsToActivate[index].SetActive(true);
        }
    }
}

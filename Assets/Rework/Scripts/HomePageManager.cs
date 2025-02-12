using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePageManager : MonoBehaviour
{
    public GameObject objectsAvtivate;
    public GameObject[] objectsToDeactivate;

    private bool hasDeactivated = false;

    void Update()
    {
        // Check if objectsAvtivate is active
        if (objectsAvtivate.activeSelf && !hasDeactivated)
        {
            // Deactivate all objects in objectsToDeactivate array
            foreach (GameObject obj in objectsToDeactivate)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }

            // Mark as deactivated
            hasDeactivated = true;
        }
        else if (!objectsAvtivate.activeSelf)
        {
            // Reset the flag when objectsAvtivate is deactivated
            hasDeactivated = false;
        }
    }
}

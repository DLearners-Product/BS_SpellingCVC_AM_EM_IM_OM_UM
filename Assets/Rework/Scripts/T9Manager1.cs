using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class T9Manager1 : MonoBehaviour
{  public GameObject[] questions;
    private int currentQuestionIndex = -1; // Start from -1 to ensure the first question is instantiated correctly
    private GameObject currentQuestionObject;

    public Transform parentTransform; // Assign the main parent object here
    public Button nextButton;
    public Button previousButton;

    public GameObject activityCompleted;
    public ParticleSystem[] particles;

    void Start()
    {
       // Initialize the first question
        currentQuestionIndex = 0;
        SetActiveQuestion(currentQuestionIndex);

        // Disable the previous button at the start
        previousButton.interactable = false;

        // Ensure the activity completed GameObject is inactive at the start
        activityCompleted.SetActive(false);
    }

    public void NextQuestion()
    {
         // Check if we are currently on the last question
        if (currentQuestionIndex == questions.Length - 1)
        {
            // Set the activity completed GameObject active
            activityCompleted.SetActive(true);
            return;
        }

        // Increment the current question index
        currentQuestionIndex++;
        StartCoroutine(ParticlesPlay());

        // Disable the current question
        SetActiveQuestion(currentQuestionIndex - 1, false);

        // Enable the next question
        SetActiveQuestion(currentQuestionIndex, true);

        // Enable the previous button after moving to the next question
        previousButton.interactable = true;

        // Check if the next question is the last one to disable the next button
        nextButton.interactable = currentQuestionIndex < questions.Length - 1;
    }

    // private void InstantiateQuestion(int index)
    // {
    //     // Instantiate the new question as a child of the main parent
    //     currentQuestionObject = Instantiate(questions[index], parentTransform);
    //     currentQuestionObject.SetActive(true); // Ensure the new question is active
    // }

    private void DestroyCurrentQuestion()
    {
        // Destroy the current question if it exists
        if (currentQuestionObject != null)
        {
            Destroy(currentQuestionObject);
        }
    }

    public void PreviousQuestion()
    {
        // Decrement the current question index
        currentQuestionIndex--;
        StartCoroutine(ParticlesPlay());

        // Check if we have reached the first question
        if (currentQuestionIndex <= 0)
        {
            // If so, disable the previous button
            previousButton.interactable = false;
        }

        // Check if we have reached the beginning of the questions array
        if (currentQuestionIndex < 0)
        {
            // If so, prevent navigating to previous questions
            Debug.Log("Already at the first question!");
            return;
        }

        // Disable the current question
        SetActiveQuestion(currentQuestionIndex + 1, false);

        // Enable the previous question
        SetActiveQuestion(currentQuestionIndex, true);

        // Enable the next button after moving to the previous question
        nextButton.interactable = true;

        // Ensure the activity completed GameObject is inactive when going back
      //  activityCompleted.SetActive(false);
    }


    IEnumerator ParticlesPlay()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (var Particles in particles)
        {
            Particles.Play();
        }
    }

     private void SetActiveQuestion(int index, bool isActive = true)
    {
        if (index >= 0 && index < questions.Length)
        {
            questions[index].SetActive(isActive);
        }
    }

 
}

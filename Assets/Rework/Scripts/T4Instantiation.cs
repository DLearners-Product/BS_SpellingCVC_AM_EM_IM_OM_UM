using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class T4Instantiation : MonoBehaviour
{
   public GameObject[] questions;
    public Button nextButton;
    public Button backButton;
    public GameObject finalObject; // The GameObject to activate after the last question

    private int currentQuestionIndex = 0;
    private GameObject currentQuestion;

    void Start()
    {
        // Start by displaying the first question
        ShowQuestion(0);

        // Set up the button listeners
        nextButton.onClick.AddListener(ShowNextQuestion);
        backButton.onClick.AddListener(ShowPreviousQuestion);

        // Initialize button states
        backButton.interactable = false; // Back button is disabled at the start
    }

    void ShowQuestion(int index)
    {
        if (currentQuestion != null)
        {
            currentQuestion.SetActive(false); // Deactivate the current question
        }

        // Instantiate and display the new question
        currentQuestion = Instantiate(questions[index], transform);
        currentQuestionIndex = index;

        // Update button states
        backButton.interactable = currentQuestionIndex > 0;
    }

    void ShowNextQuestion()
    {
        if (currentQuestionIndex < questions.Length - 1)
        {
            ShowQuestion(currentQuestionIndex + 1);
        }
        else
        {
            // If it's the last question, deactivate the current question and activate the final object
            if (currentQuestion != null)
            {
                currentQuestion.SetActive(false); // Deactivate the current question
            }

            if (finalObject != null)
            {
                finalObject.SetActive(true); // Activate the final GameObject
            }
        }
    }

    void ShowPreviousQuestion()
    {
        if (currentQuestionIndex > 0)
        {
            ShowQuestion(currentQuestionIndex - 1);
        }
    }
}

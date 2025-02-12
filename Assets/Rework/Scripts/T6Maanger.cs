using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class T6Maanger : MonoBehaviour
{
    #region unity reference variables
    //==================================================================================================

    [Header("TEXTMESHPRO---------------------------------------------------------")]
    [SerializeField] private TextMeshProUGUI TXT_Current;
    [SerializeField] private TextMeshProUGUI TXT_Total;


    [Space(10)]
    [Header("GAME OBJECT---------------------------------------------------------")]
    // [SerializeField] private GameObject[] GA_DropObjects;
    [SerializeField] private GameObject[] GA_DragObjects;
    [SerializeField] private GameObject G_TransparentScreen;
    [SerializeField] private GameObject G_ActivityCompleted;
    public static T6Maanger instance;

    //int q1Index;


    [Space(10)]
    [Header("PARTICLES---------------------------------------------------------")]
    // [SerializeField] public ParticleSystem PS_Drag;
    // [SerializeField] private ParticleSystem PS_CorrectAnswer;



    //!end of region - unity reference variables
    //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    #endregion




    #region local variables
    //==================================================================================================

    private int _currentIndex;
    private int correctDropCount = 0; // Track correct drops per question
    private const int totalDropsPerQuestion = 3; // Each question has 3 objects

    //!end of region - local variables
    //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    #endregion




    #region gameplay logic
    //==================================================================================================

    // private void Start() => TXT_Total.text = GA_Objects.Length.ToString();
    // #region QA
    // private int qIndex;
    // public GameObject questionGO;
    // public GameObject[] optionsGO;
    // public bool isActivityCompleted = false;
    // public Dictionary<string, Component> additionalFields;
    // Component question;
    // Component[] options;
    // Component[] answers;
    // #endregion


    void Start()
    {
        if (instance == null)
            instance = this;

        Debug.Log("In Start", gameObject);
        _currentIndex = 0;
        TXT_Total.text = GA_DragObjects.Length.ToString();
    }


    private IEnumerator IENUM_CorrectAnswer(string answer, Vector3 pos)
    {
        // * Correct answer logic

        yield return new WaitForSeconds(2f);

        correctDropCount++; // Increment correct answer count

        if (correctDropCount >= totalDropsPerQuestion)
        {
            Invoke(nameof(SwitchToNextQuestion), 0.5f); // Delay before switching to next question
        }

        yield return null;
    }

    private void UpdateCounter()
    {
        TXT_Current.text = (_currentIndex + 1).ToString();
        G_TransparentScreen.SetActive(false);
    }
    private void SwitchToNextQuestion()
    {
        correctDropCount = 0; // Reset the correct answer counter
        _currentIndex++;

        if (_currentIndex >= GA_DragObjects.Length)
        {
            ShowActivityCompleted();
        }
        else
        {
            GA_DragObjects[_currentIndex-1].SetActive(false);
            GA_DragObjects[_currentIndex].SetActive(true);
            UpdateCounter();
        }
    }


    public void CorrectAnswer(string answer, Vector3 pos)
    {
        StartCoroutine(IENUM_CorrectAnswer(answer, pos));


    }

    public void ReportCorrectAnswer(string selectedObject)
    {
        // ScoreManager.instance.RightAnswer(q1Index, questionID: question.id, answerID: GetOptionID(selectedObject));
        // q1Index++;
        // if (qIndex < GA_DragObjects.Length - 1)
        // {
        //     qIndex++;
        //     GetData(qIndex);
        // }




    }

    public void ReportWrongAnswer(string selectedObject)
    {
        //  ScoreManager.instance.WrongAnswer(q1Index, questionID: question.id, answerID: GetOptionID(selectedObject));
    }


    private void PlayParticles(Vector3 pos)
    {
        // PS_CorrectAnswer.transform.position = pos;
        // PS_CorrectAnswer.Play();
    }


    public void WrongAnswer(string answer)
    {

    }


    public void SetDragParticlesPosition(Transform parent)
    {
        // PS_Drag.transform.SetParent(parent);
        // PS_Drag.GetComponent<RectTransform>().localPosition = Vector3.zero;
    }


    private void ShowActivityCompleted()
    {
        G_ActivityCompleted.SetActive(true);
        //  BlendedOperations.instance.NotifyActivityCompleted();
    }



    //!end of region - gameplay logic
    //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    #endregion

    // #region QA

    // int GetOptionID(string selectedOption)
    // {
    //     for (int i = 0; i < options.Length; i++)
    //     {
    //         if (options[i].text == selectedOption)
    //         {
    //             Debug.Log(selectedOption);
    //             return options[i].id;
    //         }
    //     }
    //     return -1;
    // }

    // bool CheckOptionIsAns(Component option)
    // {
    //     for (int i = 0; i < answers.Length; i++)
    //     {
    //         if (option.text == answers[i].text) { return true; }
    //     }
    //     return false;
    // }

    // void GetData(int questionIndex)
    // {
    //     question = QAManager.instance.GetQuestionAt(0, questionIndex);
    //     // if(question != null){
    //     options = QAManager.instance.GetOption(0, questionIndex);
    //     answers = QAManager.instance.GetAnswer(0, questionIndex);
    //     // }
    // }

    // void GetAdditionalData()
    // {
    //     additionalFields = QAManager.instance.GetAdditionalField(0);
    // }

    // void AssignData()
    // {
    //     // Custom code
    //     for (int i = 0; i < optionsGO.Length; i++)
    //     {
    //         optionsGO[i].GetComponent<Image>().sprite = options[i]._sprite;
    //         optionsGO[i].tag = "Untagged";
    //         Debug.Log(optionsGO[i].name, optionsGO[i]);
    //         if (CheckOptionIsAns(options[i]))
    //         {
    //             optionsGO[i].tag = "answer";
    //         }
    //     }
    //     // answerCount.text = "/"+answers.Length;
    // }

    // #endregion
}

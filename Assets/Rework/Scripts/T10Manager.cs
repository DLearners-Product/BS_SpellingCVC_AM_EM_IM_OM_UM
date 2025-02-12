using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class T10Manager : MonoBehaviour
{
    #region unity reference variables
    //==================================================================================================

    [Header("TEXTMESHPRO---------------------------------------------------------")]
    [SerializeField] private TextMeshProUGUI TXT_Current;

    public static T10Manager instance;
    // [SerializeField] private Text TXT_Total;


    [Space(10)]
    [Header("GAME OBJECT---------------------------------------------------------")]
    // [SerializeField] private GameObject[] GA_DropObjects;
    [SerializeField] private GameObject[] GA_DragObjects;
    // [SerializeField] private GameObject G_TransparentScreen;
    // [SerializeField] private GameObject G_ActivityCompleted;


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

    int q1Index;

    //!end of region - local variables
    //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    #endregion




    #region gameplay logic
    //==================================================================================================

    // private void Start() => TXT_Total.text = GA_Objects.Length.ToString();

    public GameObject[] objs;

    public GameObject activityCompleted;

//    #region QA
//     private int qIndex;
//     public GameObject questionGO;
//     public GameObject[] optionsGO;
//     public bool isActivityCompleted = false;
//     public Dictionary<string, Component> additionalFields;
//     Component[] questions;
//     Component[] options;
//     Component[] answers;
//     #endregion


    void Start()
    {
        if (instance == null)
            instance = this;
        _currentIndex = 0;
        // #region DataSetter
        // Main_Blended.OBJ_main_blended.levelno = 4;
        // QAManager.instance.UpdateActivityQuestion();
        // qIndex = 0;
        // GetData(qIndex);
        // GetAdditionalData();
        // AssignData();
        // #endregion
        //  TXT_Total.text = GA_DragObjects.Length.ToString();
    }

    void Update()
    {
        // Check if all objects are active
        bool allActive = true;

        foreach (GameObject obj in objs)
        {
            if (!obj.activeSelf) // If any object is not active, set allActive to false
            {
                allActive = false;
                break; // No need to check further
            }
        }

        // If all objects are active, print "Game Over" and disable this script
        if (allActive)
        {
            Debug.Log("Game Over");
            enabled = false; // Disable this script to prevent repetitive logging
           //  BlendedOperations.instance.NotifyActivityCompleted();
            activityCompleted.SetActive(true);
        }
    }


    private IEnumerator IENUM_CorrectAnswer(string answer, Vector3 pos)
    {
        //*correct answer
        //TODO: additional functionality for correct answer





        //--------------------------------------------

        // PlayParticles(pos);
        yield return new WaitForSeconds(4.5f);
        _currentIndex++;
        // if (_currentIndex > 0)
        // {
        //     GA_DragObjects[_currentIndex - 1].SetActive(false);
        // }


        if (_currentIndex == GA_DragObjects.Length)
        {
            Invoke(nameof(ShowActivityCompleted), 2f);
        }
        else
        {
            //enabling current question
            //    GA_DragObjects[_currentIndex].SetActive(true);
            UpdateCounter();

        }

        yield return null;
    }

  public void ReportCorrectAnswer(string selectedObject, string questionObjectName, int questionIndex)
    {
        Debug.Log(questionObjectName);
        // ScoreManager.instance.RightAnswer(q1Index, questionID: GetQuestionID(questionObjectName), answerID: GetOptionID(selectedObject));
        // q1Index++;
        // if (qIndex < GA_DragObjects.Length - 1)
        // {
        //   //  qIndex++;
        //     GetData(questionIndex);
        // }




    }
    public void ReportWrongAnswer(string selectedObject,string questionObjectName,int questionIndex)
    {
        Debug.Log(questionObjectName);
        // ScoreManager.instance.WrongAnswer(q1Index, questionID: GetQuestionID(questionObjectName), answerID: GetOptionID(selectedObject));
        //   if (qIndex < GA_DragObjects.Length - 1)
        // {
        //   //  qIndex++;
        //     GetData(questionIndex);
        // }

    }


    private void UpdateCounter()
    {

        TXT_Current.text = (_currentIndex + 1).ToString();

        //animation
        // Utilities.Instance.ANIM_ClickEffect(TXT_Current.transform.parent);

        //enabling user interaction
        // G_TransparentScreen.SetActive(false);
    }


    public void CorrectAnswer(string answer, Vector3 pos)
    {
        StartCoroutine(IENUM_CorrectAnswer(answer, pos));
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
        // G_ActivityCompleted.SetActive(true);
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
    //    int GetQuestionID(string selectedQuestion)
    // {
    //     for (int i = 0; i < questions.Length; i++)
    //     {
    //         if (questions[i].text == selectedQuestion)
    //         {
    //             Debug.Log(selectedQuestion);
    //             return questions[i].id;
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
    //     questions = QAManager.instance.GetAllQuestions(0);
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

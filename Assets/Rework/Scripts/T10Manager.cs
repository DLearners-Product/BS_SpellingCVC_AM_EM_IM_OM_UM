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



    #region QA

    private int qIndex;
    public GameObject questionGO;
    public GameObject[] optionsGO;
    public Dictionary<string, Component> additionalFields;
    Component question;
    Component[] options;
    Component[] answers;

    #endregion



    void Start()
    {
        if (instance == null)
            instance = this;
        _currentIndex = 0;

        #region DataSetter
        //Main_Blended.OBJ_main_blended.levelno = 3;
        QAManager.instance.UpdateActivityQuestion();
        qIndex = 0;
        GetData(qIndex);
        GetAdditionalData();
        AssignData();
        #endregion

    }

    // void Update()
    // {
    //     // Check if all objects are active
    //     bool allActive = true;

    //     foreach (GameObject obj in objs)
    //     {
    //         if (!obj.activeSelf) // If any object is not active, set allActive to false
    //         {
    //             allActive = false;
    //             break; // No need to check further
    //         }
    //     }

    //     // If all objects are active, print "Game Over" and disable this script
    //     if (allActive)
    //     {
    //         Debug.Log("Game Over");
    //         enabled = false; // Disable this script to prevent repetitive logging
    //                          //  BlendedOperations.instance.NotifyActivityCompleted();
    //         activityCompleted.SetActive(true);
    //     }
    // }


    private IEnumerator IENUM_CorrectAnswer()
    {
        //*correct answer


        yield return new WaitForSeconds(4.5f);
        _currentIndex++;

        if (_currentIndex == objs.Length)
        {
            Invoke(nameof(ShowActivityCompleted), 2f);
        }
        else
        {
            UpdateCounter();
        }

        yield return null;
    }

    public void ReportCorrectAnswer(int index, string ans)
    {
        //?scoring integration
        GetData(index);

        ScoreManager.instance.RightAnswer(qIndex, questionID: question.id, answerID: GetOptionID(ans));

        if (qIndex < objs.Length - 1)
        {
            qIndex++;
        }
    }


    public void ReportWrongAnswer(int index, string ans)
    {
        //?scoring integration
        GetData(index);

        ScoreManager.instance.WrongAnswer(qIndex, questionID: question.id, answerID: GetOptionID(ans));
    }


    private void UpdateCounter()
    {
        TXT_Current.text = (_currentIndex + 1).ToString();
    }


    public void CorrectAnswer()
    {
        StartCoroutine(IENUM_CorrectAnswer());
    }


    private void ShowActivityCompleted()
    {
        BlendedOperations.instance.NotifyActivityCompleted();
        activityCompleted.SetActive(true);
    }



    //!end of region - gameplay logic
    //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    #endregion



    #region QA

    int GetOptionID(string selectedOption)
    {
        for (int i = 0; i < options.Length; i++)
        {
            if (options[i].text == selectedOption)
            {
                return options[i].id;
            }
        }
        return -1;
    }

    bool CheckOptionIsAns(Component option)
    {
        for (int i = 0; i < answers.Length; i++)
        {
            if (option.text == answers[i].text) { return true; }
        }
        return false;
    }

    void GetData(int questionIndex)
    {
        Debug.Log(">>>>>" + questionIndex);
        question = QAManager.instance.GetQuestionAt(0, questionIndex);
        //if(question != null){
        options = QAManager.instance.GetOption(0, questionIndex);
        answers = QAManager.instance.GetAnswer(0, questionIndex);
        // }
    }

    void GetAdditionalData()
    {
        additionalFields = QAManager.instance.GetAdditionalField(0);
    }

    void AssignData()
    {
        // Custom code
        for (int i = 0; i < optionsGO.Length; i++)
        {
            optionsGO[i].GetComponent<Image>().sprite = options[i]._sprite;
            optionsGO[i].tag = "Untagged";
            Debug.Log(optionsGO[i].name, optionsGO[i]);
            if (CheckOptionIsAns(options[i]))
            {
                optionsGO[i].tag = "answer";
            }
        }
        // answerCount.text = "/"+answers.Length;
    }

    #endregion

}

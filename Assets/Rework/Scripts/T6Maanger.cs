using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Text;



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



    //!end of region - unity reference variables
    //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    #endregion




    #region local variables
    //==================================================================================================

    private int _currentIndex;
    private int correctDropCount = 0; // Track correct drops per question
    private const int totalDropsPerQuestion = 3; // Each question has 3 objects

    private StringBuilder _sb;

    //!end of region - local variables
    //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    #endregion



    #region QA

    private int qIndex;
    public GameObject questionGO;
    public GameObject[] optionsGO;
    public Dictionary<string, Component> additionalFields;
    Component question;
    Component[] options;
    Component[] answers;

    #endregion



    #region gameplay logic
    //==================================================================================================

    void Start()
    {
        if (instance == null)
            instance = this;


        _currentIndex = 0;
        TXT_Total.text = GA_DragObjects.Length.ToString();
        _sb = new StringBuilder();
        ResetStringBuilder();


        #region DataSetter
        //Main_Blended.OBJ_main_blended.levelno = 3;
        QAManager.instance.UpdateActivityQuestion();
        qIndex = 0;
        GetData(qIndex);
        GetAdditionalData();
        AssignData();
        #endregion

    }


    private void ResetStringBuilder()
    {
        _sb.Clear();

        _sb.Append("_");
        _sb.Append("_");
        _sb.Append("_");
    }


    private IEnumerator IENUM_CorrectAnswer(string answer, Vector3 pos)
    {
        // * Correct answer logic

        yield return new WaitForSeconds(2f);

        correctDropCount++;

        if (correctDropCount >= totalDropsPerQuestion)
        {
            //?scoring integration
            ScoreManager.instance.RightAnswer(qIndex, questionID: question.id, answer: _sb.ToString());

            if (qIndex < GA_DragObjects.Length - 1)
                qIndex++;

            GetData(qIndex);

            Invoke(nameof(SwitchToNextQuestion), 0.5f);
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
        correctDropCount = 0;
        _currentIndex++;

        if (_currentIndex >= GA_DragObjects.Length)
        {
            ShowActivityCompleted();
        }
        else
        {
            GA_DragObjects[_currentIndex - 1].SetActive(false);
            GA_DragObjects[_currentIndex].SetActive(true);
            UpdateCounter();
        }

        ResetStringBuilder();
    }


    public void CorrectAnswer(string answer, Vector3 pos, int index)
    {
        StartCoroutine(IENUM_CorrectAnswer(answer, pos));

        _sb.Remove(index, 1);
        _sb.Insert(index, answer);

        Debug.Log(_sb.ToString());
    }


    public void WrongAnswer(string answer, int index)
    {
        _sb.Remove(index, 1);
        _sb.Insert(index, answer);

        Debug.Log(_sb.ToString());

        //?scoring integration
        ScoreManager.instance.WrongAnswer(qIndex, questionID: question.id, answer: _sb.ToString());
    }


    private void ShowActivityCompleted()
    {
        BlendedOperations.instance.NotifyActivityCompleted();
        G_ActivityCompleted.SetActive(true);
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
                Debug.Log(selectedOption);
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
        question = QAManager.instance.GetQuestionAt(0, questionIndex);
        // if(question != null){
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

    }

    #endregion

}

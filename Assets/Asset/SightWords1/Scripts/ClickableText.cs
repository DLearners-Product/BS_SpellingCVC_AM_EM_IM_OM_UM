using UnityEngine;
using TMPro;
using System.Collections;
using SightWords2;
using UnityEngine.UI;
using TMPro;


public class ClickableText : MonoBehaviour
{
    [SerializeField] private string answer;
    [SerializeField] private Color correctColor = Color.green; // Color for the correct answer
    [SerializeField] private Color incorrectColor = Color.red; // Color for the incorrect answer
    [SerializeField] private float revertDelay = 2f; // Time in seconds to revert back to the original color

    private Camera cam;
    private TextMeshProUGUI text;
    private string originalText;
    private string lastClickedWord;
    private StoryTime REF_StoryTime;

    [SerializeField] private GameObject[] GA_Objects;


    [SerializeField] private GameObject G_ActivityCompleted;

    private int _currentIndex;


    public int correctanswercount;

    public TextMeshProUGUI counter;


    void Start()
    {
        cam = Camera.main;
        text = GetComponent<TextMeshProUGUI>();
        originalText = text.text; // Save the original text
        REF_StoryTime = FindObjectOfType<StoryTime>();
        REF_StoryTime.RegisterClickableText(this);
    }


    void OnDestroy()
    {
        if (REF_StoryTime != null)
        {
            REF_StoryTime.UnregisterClickableText(this);
        }
    }


    public void SetClickable(bool value)
    {
        enabled = value;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int wordIndex = TMP_TextUtilities.FindIntersectingWord(text, Input.mousePosition, cam);

            if (wordIndex != -1)
            {
                lastClickedWord = text.textInfo.wordInfo[wordIndex].GetWord();
                Check(wordIndex);
            }
        }
    }


    private void Check(int wordIndex)
    {
        if (lastClickedWord.ToLower().Equals(answer.ToLower()))
        {
            HighlightWord(wordIndex, correctColor);

            StopAllCoroutines();
            SetClickable(false);
        }
        else
        {
            REF_StoryTime.THI_WrongAnswer();
            HighlightWord(wordIndex, incorrectColor);

            REF_StoryTime.ReportWrongAnswer(lastClickedWord);
            StartCoroutine(RevertColor());
        }
    }


    private void HighlightWord(int wordIndex, Color color)
    {
        TMP_WordInfo wordInfo = text.textInfo.wordInfo[wordIndex];

        string word = wordInfo.GetWord();
        string colorTag = $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{word}</color>";

        int startIndex = wordInfo.firstCharacterIndex;
        int length = wordInfo.characterCount;

        string preText = originalText.Substring(0, startIndex);
        string postText = originalText.Substring(startIndex + length);

        text.text = preText + colorTag + postText;
    }


    public void THI_CorrectAnswer(Transform obj)
    {
        obj.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        correctanswercount++;
        REF_StoryTime.ReportCorrectanswer(obj.gameObject.name);
        int currentCounterValue = int.Parse(counter.text);
        if (currentCounterValue <= 15)
        {
            currentCounterValue++;
            counter.text = currentCounterValue.ToString();
        }

        if (correctanswercount == 15)
        {
            StartCoroutine(EnableActivityCompleted());
        }

        _currentIndex++;
        obj.gameObject.GetComponent<Button>().interactable = false;
        obj.gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;
    }


    private IEnumerator EnableActivityCompleted()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(2f);

        // Enable the GameObject after the delay
        if (G_ActivityCompleted != null)
        {
            G_ActivityCompleted.SetActive(true);
            this.gameObject.SetActive(false);
            Debug.Log("Activity Completed Screen Enabled!");
            //BlendedOperations.instance.NotifyActivityCompleted();
        }
        else
        {
            Debug.LogWarning("G_ActivityCompleted is not assigned in the Inspector.");
        }
    }


    private IEnumerator RevertColor()
    {
        yield return new WaitForSeconds(revertDelay);

        text.text = originalText;
    }

}

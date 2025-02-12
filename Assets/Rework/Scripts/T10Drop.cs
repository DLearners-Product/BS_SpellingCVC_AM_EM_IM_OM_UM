using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
public class T10Drop :  MonoBehaviour, IDropHandler
{
    private T10Manager REF_DragnDrop_V1;
    private Vector3 initialPosition, currentPosition;
    private float elapsedTime, desiredDuration = 0.2f;
   
    public AudioSource source;
    public AudioClip correctAnswer;

    public AudioClip wrongAnswer;

    // public GameObject text;

    // public GameObject[] OneObj;


    //  public Text counter;

    // private int oneObjIndex = 0; // Track the current index for OneObj array
    // private int twoObjIndex = 0; // Track the current index for TwoObj array


    void Start()
    {
        REF_DragnDrop_V1 = FindObjectOfType<T10Manager>();
        initialPosition = transform.position;
    }


    public void OnDrop(PointerEventData eventData)
    {
        T10Drag drag = eventData.pointerDrag.GetComponent<T10Drag>();

        //MATCHING USING THE DRAG AND DROP GAMEOBJECT NAME
        //*correct answer
        if (drag.name == gameObject.name)
        {
            drag.isDropped = true;
            StartCoroutine(IENUM_LerpTransform(drag.rectTransform, drag.rectTransform.anchoredPosition, GetComponent<RectTransform>().anchoredPosition));

            REF_DragnDrop_V1.CorrectAnswer(drag.name, transform.position);
            Debug.Log("correctAnswer");
          //  T10Manager.instance.ReportCorrectAnswer(drag.name);

            source.clip = correctAnswer;
            source.Play();




        }
        //!wrong answer
        else
        {
            REF_DragnDrop_V1.WrongAnswer(drag.name);
         //   T10Manager.instance.ReportWrongAnswer(drag.name);

            source.clip = wrongAnswer;
            source.Play();
            

           //  StartCoroutine(WrongAnswerColor());
        }

    }


    IEnumerator IENUM_LerpTransform(RectTransform obj, Vector3 currentPosition, Vector3 targetPosition)
    {
        while (elapsedTime < desiredDuration)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / desiredDuration;

            //  obj.anchoredPosition = Vector3.Lerp(currentPosition, targetPosition, percentageComplete);
            yield return null;
        }

        //setting parent
        // obj.transform.SetParent(transform);
        // this.transform.GetChild(2).GetComponent<ParticleSystem>().Play();
        obj.gameObject.SetActive(false);
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(1f);
      
        // obj.transform.localPosition = Vector2.zero;

        //resetting elapsed time back to zero
        elapsedTime = 0f;
    }

}

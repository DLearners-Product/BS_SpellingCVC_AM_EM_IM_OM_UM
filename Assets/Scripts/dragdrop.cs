using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dragdrop : MonoBehaviour
{
    Vector2 startpos;
    public bool B_isDragging;
    public bool B_correctMatch;
    public GameObject G_collisionObject;

    private void Start()
    {
        B_correctMatch = false;
        startpos = this.gameObject.transform.position;
    }
    void Update()
    {
        if (B_isDragging)
        {
            Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.gameObject.transform.position = mousepos;
        }
        else
        {
            if (!B_correctMatch)
            {
                this.gameObject.transform.position = startpos;

            }
            else
            {
                this.gameObject.transform.position = G_collisionObject.transform.GetChild(0).transform.position;
                if(dragdropmain.OBJ_ddmain.SPR_right!=null)
                G_collisionObject.GetComponent<Image>().sprite = dragdropmain.OBJ_ddmain.SPR_right;
                dragdropmain.OBJ_ddmain.AS_correct.Play();
                dragdropmain.OBJ_ddmain.I_matchCount++;
                if (dragdropmain.OBJ_ddmain.GameName == "slide6")
                {
                    if (dragdropmain.OBJ_ddmain.I_matchCount == 3)
                    {
                        dragdropmain.OBJ_ddmain.THI_delayQuestion();
                    }
                }
                else
                {
                    if (dragdropmain.OBJ_ddmain.I_matchCount == 2)
                    {
                        dragdropmain.OBJ_ddmain.THI_delayQuestion();
                    }
                }
                Destroy(this.gameObject.GetComponent<dragdrop>());
            }
        }
    }
    public void OnMouseDown()
    {
        B_isDragging = true;
    }
    public void OnMouseUp()
    {
        B_isDragging = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (this.gameObject.name == collision.gameObject.name)
        {
            B_correctMatch = true;
            G_collisionObject = collision.gameObject;
           
        }
      
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.gameObject.name == collision.gameObject.name)
        {
            B_correctMatch = false;
            G_collisionObject = null;
           
        }
    }
}
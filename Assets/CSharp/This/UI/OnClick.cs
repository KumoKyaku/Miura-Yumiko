using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using UnityEngine.UI;

public class OnClick : MonoBehaviour,IPointerClickHandler
{
    public Sprite[] list;
    public Image image;
    int i = 0;
    private bool IsClick;
    private bool isMax;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsClick)
        {
            StartCoroutine(TimeOn());
        }
        else
        {
            IsClick = false;
            if (isMax)
            {
                isMax = false;
                Min();
            }
            else
            {
                isMax = true;
                Max();
            }
        }   
    }

    private void Max()
    {
        var tr = transform as RectTransform;
        tr.sizeDelta = new Vector2(1280,720);
        tr.position = Vector3.zero;
    }

    private void Min()
    {
        var tr = transform as RectTransform;
        tr.sizeDelta = new Vector2(512, 288);
        tr.localPosition = Vector3.zero;
    }

    private IEnumerator TimeOn()
    {
        IsClick = true;
        yield return new WaitForSeconds(0.3f);
        if (IsClick)
        {
            IsClick = false;
            Next();
        } 
    }

    // Use this for initialization
    void Start () {
        if (list.Length != 0)
        {
            image.sprite = list[i];
        }
	}

    void Next()
    {
        if (list.Length == 0)
        {
            return;
        }
        i++;
        if (i >= list.Length)
        {
            i = 0; Next();
        }
        else
        {
            image.sprite = list[i];
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Poi;
using System;

public class Option : MonoBehaviour,iLabel
{
    [SerializeField]
    private RectTransform rect;
    [SerializeField]
    private Image image;
    [SerializeField]
    private UnityEngine.UI.Text message;

    public int ID
    {
        get
        ;

        set;
    }

    public string Name
    {
        get
        ;

        set

            ;
    }

    public RectTransform Rect
    {
        get
        {
            return rect;
        }
    }

    public Image Image
    {
        get
        {
            return image;
        }
    }

    public UnityEngine.UI.Text Message
    {
        get
        {
            return message;
        }
    }

    public Action<Option> Callback
    {
        get
        {
            return callback;
        }

        set
        {
            callback = value;
        }
    }

    public OptionState OptionState { get; set; }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private Action<Option> callback;
    public void Click()
    {
        if (callback!= null)
        {
            callback(this);
        }
    }   
}
public enum OptionState
{
    Default,
}
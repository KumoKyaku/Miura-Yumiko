using UnityEngine;
using System.Collections;
using Poi;
using System;
using UnityEngine.UI;

public class UI_Message : UI_Base
{
    public override void Help()
    {
        throw new NotImplementedException();
    }

    public override void Use(Context _con)
    {
        throw new NotImplementedException();
    }
    [SerializeField]
    private UnityEngine.UI.Text title;
    [SerializeField]
    private UnityEngine.UI.Text message;
    private Action<UI_Message> Callback;

    public UnityEngine.UI.Text Title
    {
        get
        {
            return title;
        }
    }

    public UnityEngine.UI.Text Message
    {
        get
        {
            return message;
        }
    }

    // Use this for initialization
    void Start()
    {
        SetUICamera(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Use(UI_Message_Context _con)
    {
        this.Callback = _con.CallBack;
        title.text = _con.Title;
        message.text = _con.Message;
        SetFront();
        gameObject.SetActive(true);
    }

    public override void UseDone()
    {
        base.UseDone();
        if (Callback != null)
        {
            Callback(this);
        }
    }


}
public class UI_Message_Context
{
    public string Message { get; internal set; }
    public string Title { get; internal set; }
    public Action<UI_Message> CallBack { get; set; }
}
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Poi;

public class UI_Ask : UI_Base {

    [SerializeField]
    private UnityEngine.UI.Text message;

    Action<bool, UI_Ask> callback;
    Action helpCallback;
    string helpMessage;
    public UnityEngine.UI.Text Message
    {
        get
        {
            return message;
        }
    }

    // Use this for initialization
    void Start () {
        SetUICamera(this);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Yes()
    {
        if (callback!= null)
        {
            callback(true,this);
        }
    }

    public void No()
    {
        if (callback!= null)
        {
            callback(false, this);
        }
        else
        {
            UseDone();
        }
    }

    public void Use(UI_Ask_Context _con)
    {
        Message.text = _con.Message;
        callback = _con.Callback;
        helpCallback = _con.HelpCallback;
        helpMessage = _con.HelpMessage;
        gameObject.SetActive(true);
    }

    public override void UseDone()
    {
        base.UseDone();
        helpCallback = null;
    }

    public override void Help()
    {
        if (helpCallback != null)
        {
            helpCallback();
        }
        else
        {
            if (string.IsNullOrEmpty(helpMessage))
            {
                helpMessage = Poi.Writing.Get(200002);
            }

            UIManager.GetUI<UI_Message>().Use(new UI_Message_Context()
            {
                Title = Poi.Writing.Get(100003),
                Message = helpMessage
            });
        }
    }

    public override void Use(Context _con)
    {
        
    }
}
public class UI_Ask_Context
{
    public Action<bool, UI_Ask> Callback { get; internal set; }
    public Action HelpCallback { get; internal set; }
    public string HelpMessage { get; internal set; }
    public string Message { get; internal set; }
}
using UnityEngine;
using System.Collections;
using Poi;
using System;
using System.Collections.Generic;
using AVG;
using System.Linq;

public class UI_Start : UI_Base
{
    // Use this for initialization
    void Start () {
        SetUICamera(this);
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void Begin()
    {
        UIManager.SystemUI.Log("\n屏幕高度" + Screen.height);
        UIManager.SystemUI.Log("\n屏幕高度" + Screen.width);
        GameManager.LoadLevel("Game");
        GameManager.AVGStart(1);
        UseDone();
    }

    public void Set()
    {
        UIManager.GetUI<UI_Config>().Use();
    }

    public void Exit()
    {
        GameManager.Exit(1);
    }

    public void Continue()
    {
        UI_Choice ch = UIManager.GetUI<UI_Choice>();

        UI_Choice_Context con = new UI_Choice_Context();
        con.OptionList = new List<iLabel>();

        for (int i = 0; i < GameManager.Save.File.Length; i++)
        {
            con.OptionList.Add(new Label()
            {
                ID = i,
                Name = @"[" + Writing.Get(100027) + (i + 1) + "]:" +
                Writing.Get(Libretto.GetNode(GameManager.Save.File[i]).Sentences.First().Value.DialogueID),
            });
        }

        con.Callback = this.LoadCallbacek;
        ch.Use(con);
        ch.SetFront();
    }

    private void LoadCallbacek(int arg1, UI_Choice arg2)
    {
        GameManager.LoadLevel("Game");
        GameManager.AVGStart(GameManager.Save.File[arg1]);
        UseDone();
        arg2.UseDone();
    }

    public override void Help()
    {
        throw new NotImplementedException();
    }

    public override void Use(Context _con)
    {
        gameObject.SetActive(true);
    }
}

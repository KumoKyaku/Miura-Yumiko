using UnityEngine;
using System.Collections;
using Poi;
using System;
using System.Collections.Generic;

public class UI_Game : UI_Base {
    public bool IsAuto { get; private set; }

    [SerializeField]
    private int autoSecondPer;
    [SerializeField]
    private GameObject autoEffect;

    public override void Help()
    {
        throw new NotImplementedException();
    }

    public override void Use(Context _con)
    {
        gameObject.SetActive(true);
    }

    // Use this for initialization
    void Start () {
        SetUICamera(this);
        SetFront();
        SetGap(autoSecondPer);
	}


    // Update is called once per frame
    void Update () {
        if (IsAuto)
        {
            deltaTime = DateTime.Now - startTime;
            if (deltaTime >= gap)
            {
                startTime = startTime + deltaTime;
                deltaTime = TimeSpan.Zero;
                Next();
            }
        }
	}

    public void Save()
    {
        GameManager.AVGPlayer.Save();
    }

    public void Load()
    {
        GameManager.AVGPlayer.Load();
    }

    DateTime startTime = DateTime.Now;
    private TimeSpan deltaTime;
    private TimeSpan gap;
    public void Auto()
    {
        IsAuto = !IsAuto;
        startTime = DateTime.Now;
        deltaTime = TimeSpan.Zero;
        if (autoEffect != null)
        {
            autoEffect.SetActive(IsAuto);
        }
    }

    /// <summary>
    /// 最小值
    /// </summary>
    /// <param name="autoSecondPer"></param>
    private void SetGap(int autoSecondPer)
    {
        autoSecondPer = autoSecondPer < CFG.AutoGap ? CFG.AutoGap : autoSecondPer;
        gap = new TimeSpan(0, 0, autoSecondPer);
    }

    public void Back()
    {
        EffectManager.Clear();
        GameManager.LoadLevel("Start");       
        UseDone();
    }

    public void Up()
    {
        GameManager.AVGPlayer.Captions.Last();
    }

    public void Down()
    {
        GameManager.AVGPlayer.Captions.Next();
    }

    public bool NoNext;
    public void Click()
    {
        if (IsAuto)
        {
            Auto();
        }
        Next();
    }

    void Next()
    {
        if (NoNext)
        {
            return;
        }
        GameManager.AVGPlayer.NextNode();
    }
}

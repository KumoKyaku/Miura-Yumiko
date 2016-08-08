using UnityEngine;
using System.Collections;
using Poi;
using System;
using UnityEngine.UI;

public class UI_LoadImage : UI_Base {

    private AsyncOperation progress;

    public AsyncOperation Progress
    {
        get
        {
            return progress;
        }

        set
        {
            progress = value;
        }
    }

    [SerializeField]
    private UnityEngine.UI.Text text;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Sprite[] list;

    public override void Help()
    {
        throw new NotImplementedException();
    }

    public override void Use(Context _con)
    {
        throw new NotImplementedException();
    }

    // Use this for initialization
    void Start () {
        SetUICamera(this);	
	}
	
	// Update is called once per frame
	void Update () {
        if (Progress != null && !Progress.isDone)
        {
            text.text = ((int)(Progress.progress * 100)).ToString();
        }
        else if (Progress != null && (Progress.allowSceneActivation || Progress.isDone))
        {
            UseDone();
        }
        else
        {
            text.text = "";
        }
	}

    public void Use(UI_LoadImage_Context _con)
    {
        Progress = _con.Process;
        if (_con.ImageNum == null || _con.ImageNum >= list.Length)
        {
            image.sprite = list[(new System.Random().Next(1000)) % list.Length];
        }
        else
        {
            image.sprite = list[_con.ImageNum ?? 0];
        }

        Use();
    }
}

public class UI_LoadImage_Context
{
    public int? ImageNum { get; internal set; }
    public AsyncOperation Process { get; internal set; }
}
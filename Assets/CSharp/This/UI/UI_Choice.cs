using UnityEngine;
using Poi;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class UI_Choice : UI_Base {
    public override void Help()
    {
        throw new NotImplementedException();
    }

    public override void Use(Context _con)
    {
        throw new NotImplementedException();
    }

    [SerializeField]
    private RectTransform content;
    [SerializeField]
    private GameObject Templet;

    public RectTransform Content
    {
        get
        {
            return content;
        }
    }

    List<Option> list = new List<Option>();
    private Action<int, UI_Choice> Callback;

    // Use this for initialization
    void Start () {
        SetUICamera(this);
        //Test();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Use(UI_Choice_Context _con)
    {
        this.Callback = _con.Callback;
        ClearList();
        ResetContent(_con.OptionList.Count);
        for (int i = 0; i < _con.OptionList.Count; i++)
        {
            Option _tempOption = (Instantiate(Templet) as GameObject).GetComponent<Option>();

            _tempOption.Rect.SetParent(Content);
            _tempOption.Rect.localScale = new Vector3(1, 1, 1);
            _tempOption.Rect.anchoredPosition3D = new Vector3(0,0,0);
            _tempOption.Rect.offsetMin = new Vector2(50, -((i + 1) * 150));
            _tempOption.Rect.offsetMax = new Vector2(-50, -((i + 1) * 150) + 100);

            _tempOption.Message.text = _con.OptionList[i].Name;
            _tempOption.Name = _con.OptionList[i].Name;
            _tempOption.ID = _con.OptionList[i].ID;
            _tempOption.Callback = Choose;
            _tempOption.OptionState = OptionState.Default;

            _tempOption.gameObject.SetActive(true);
            list.Add(_tempOption);
        }
        gameObject.SetActive(true);
    }

    private void ResetContent(int _count)
    {
        Content.sizeDelta = new Vector2(0,_count*150 + 50);
    }

    private void ClearList()
    {
        foreach (var item in list)
        {
            Destroy(item.gameObject);
        }
        list.Clear();
    }

    public void Choose(Option _c)
    {
        if (Callback != null)
        {
            Callback(_c.ID, this);
        }
    }

    public override void Test()
    {
        List<iLabel> _list = new List<iLabel>();
        for (int i = 0; i < 10; i++)
        {
            Label _label = new Label() { ID = i, Name = "选择" + i };
            _list.Add(_label);
        }
        Use(new UI_Choice_Context() { OptionList = _list });
    }
}

public class UI_Choice_Context
{
    public IList<iLabel> OptionList { get; set; }
    public Action<int, UI_Choice> Callback { get; set; }
}
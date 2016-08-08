using UnityEngine;
using System.Collections;
using Poi;
using System;

public class UI_Config : UI_Base {
    public override void Help()
    {

    }

    public override void Use(Context _con)
    {
        throw new NotImplementedException();
    }
    [SerializeField]
    private Rules rules;
    // Use this for initialization
    void Start () {
        SetUICamera(this);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CG()
    {
        rules.Change("CG");
    }

    public void SetLanguage()
    {
        rules.Change("Language");
    }

    public void Log()
    {
        rules.Change("SystemLog");
    }
    public void Metaphysics()
    {
        rules.Change("Metaphysics");
    }

    public void Use()
    {
        gameObject.SetActive(true);
    }

    public override void UseDone()
    {
        base.UseDone();
        //UIManager.GetUI<UI_Start>().LockOrOpen(true);
    }
}

using UnityEngine;
using System.Collections;
using Poi;
public class SystemUIRule : Rule {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Commit()
    {
        base.Commit();
    }

    public void Open()
    {
        UIManager.SystemUI.OI(true);
    }

    public void Close()
    {
        UIManager.SystemUI.OI(false);
    }
}

using UnityEngine;
using System.Collections;
using Poi;

public class MetaphysicsRule : Rule {

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

    public void SetScreen()
    {
#if UNITY_STANDALONE_WIN
        UIManager.GetUI<UI_Ask>().Use(new UI_Ask_Context() { Message = Writing.Get(100033),Callback = (_oi,_ask)=> 
            {
                if (_oi)
                {
                    Screen.SetResolution(1120, 630, false);
                }

                _ask.UseDone();
            } });
#endif
    }

    public void UseEff()
    {
        GameManager.Save.Use5MaoEffect = true;
    }

    public void UnUseEff()
    {
        GameManager.Save.Use5MaoEffect = false;
    }
}

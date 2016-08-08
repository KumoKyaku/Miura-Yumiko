using UnityEngine;
using System.Collections;
using Poi;
public class LanguageRule : Rule {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Commit()
    {
        if (Writing.CurrentLanguage != curlanguage)
        {
            StartCoroutine(GameManager.LoadText(curlanguage));
        }
    }

    Language curlanguage = Language.Chinese;
    public void CHS()
    {
        curlanguage = Language.Chinese;
    }

    public void ENG()
    {
        curlanguage = Language.English;
    }

    public void JAP()
    {
        curlanguage = Language.Japanese;
    }

    public void U1()
    {
        curlanguage = Language.Undefined1;
    }
}

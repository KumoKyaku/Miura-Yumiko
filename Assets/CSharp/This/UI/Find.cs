using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Find : MonoBehaviour {

    public SpriteRenderer spriter;
    private static Find Instance;

    void Start () {
        Instance = this;
        if (Poi.GameManager.SpBG != null)
        {
            spriter.sprite = Poi.GameManager.SpBG;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    static public void Set(Sprite s)
    {
        Instance.Setbg(s);
    }

    private void Setbg(Sprite s)
    {
        spriter.sprite = Poi.GameManager.SpBG;
    }
}

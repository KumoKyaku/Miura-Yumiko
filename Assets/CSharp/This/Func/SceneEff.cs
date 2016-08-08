using UnityEngine;
using System.Collections;
using AVG;
using Poi;
using System;

public class SceneEff : MonoBehaviour,iUseEffect
{
    public EffectPos EffectPos
    {
        get
        {
            return effectPos;
        }
    }
    [SerializeField]
    private EffectPos effectPos;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

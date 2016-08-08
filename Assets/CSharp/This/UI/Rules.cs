using UnityEngine;
using System.Collections;
using Poi;
using System;

public class Rules : MonoBehaviour {

    [SerializeField]
    private Rule[] rule;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    

    public void Commit()
    {
        if (current != null)
        {
            current.Commit();
        }
    }

    Rule current = null;
    internal void Change(string v)
    {
        foreach (var item in rule)
        {
            if (item.name == v)
            {
                item.gameObject.SetActive(true);
                current = item;
            }
            else
            {
                item.gameObject.SetActive(false);
            }
        }
    }

    
}

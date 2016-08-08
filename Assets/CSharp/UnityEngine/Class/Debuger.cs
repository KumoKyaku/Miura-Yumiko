using UnityEngine;
using System.Collections;
using Poi;

public class Debuger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    static public void Log(object _target)
    {
        if (UIManager.SystemUI)
        {
            UIManager.SystemUI.Log(_target);
        }
        Debug.Log(_target);
    }

    static public void LogError(object _target)
    {
        if (UIManager.SystemUI)
        {
            UIManager.SystemUI.Log(_target);
        }
        Debug.LogError (_target);
    }
}

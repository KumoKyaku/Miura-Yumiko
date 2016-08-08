using UnityEngine;
using System.Collections;
using System;
using Poi;

public class KeyCodeListener : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GameManager.Exit(1);
        }
    }
}

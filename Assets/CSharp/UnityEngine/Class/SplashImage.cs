using UnityEngine;
using System.Collections;
using Poi;
using System;

public class SplashImage : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Wait());
	}

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        GameManager.SplashDone = true;
    }
}

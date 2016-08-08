using UnityEngine;
using System.Collections;
using Poi;
using System;

public class LoadList : MonoBehaviour
{


    [SerializeField]
    private string[] UseUIList;
    [SerializeField]
    private GameObject[] Instantiatelist;

    public bool UILoadDone { get; set; }

    // Use this for initialization
    void Start()
    {
        foreach (var item in Instantiatelist)
        {
            Instantiate(item);
        }

        foreach (var item in UseUIList)
        {
            var _ui = UIManager.GetUI(item);
            if (_ui != null)
            {
                _ui.Use();
            }
        }

        Destroy(gameObject);
    }
}

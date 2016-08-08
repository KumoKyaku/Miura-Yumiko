using UnityEngine;
using System.Collections;
using System;
using Poi;

public class ViewEffect : MonoBehaviour {
    [HideInInspector]
    public int Life { get; internal set; }
    [HideInInspector]
    public iUseEffect owner { get; internal set; }
    [SerializeField]
    public Animator controller;

    public void SetPos(int pos)
    {
        if (pos< 0 || pos >= owner.EffectPos.Pos.Length)
        {
            transform.SetParent(owner.EffectPos.Pos[1]);
        }
        else
        {
            transform.SetParent(owner.EffectPos.Pos[pos]);
        }
        transform.localPosition = Vector3.zero;
    }

    public void SetSize(Vector3 zero)
    {
        transform.localScale = zero;
    }

    public void Play(string v,bool isLoop = true)
    {
        controller.Play(v);
        controller.SetBool("IsLoop", isLoop);
    }
}

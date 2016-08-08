using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AVG;
using System;
using Poi;
using System.Linq;
public class Charactor : MonoBehaviour {


    [SerializeField]
    private ParticleSystem _ParticleFront;
    [SerializeField]
    private ParticleSystem _Particleback;

    private Dictionary<int, MoodActor> actorList = new Dictionary<int, MoodActor>();

    public ParticleSystem ParticleFront
    {
        get
        {
            return _ParticleFront;
        }
    }

    public ParticleSystem Particleback
    {
        get
        {
            return _Particleback;
        }
    }

    public Dictionary<int, MoodActor> ActorList
    {
        get
        {
            return actorList;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    List<MoodActor> thistime = new List<MoodActor>();
    internal void Play(IList<iActor> _list)
    {
        thistime.Clear();
        foreach (var item in _list)
        {
            MoodActor _actor = GetActor(item);
            _actor.Play(item);
            thistime.Add(_actor);
        }

        foreach (var item in actorList)
        {
            if (thistime.Contains(item.Value))
            {
                continue;
            }
            item.Value.gameObject.SetActive(false);
        }
    }

    private MoodActor GetActor(iActor item)
    {
        if (actorList.ContainsKey(item.ID))
        {
            return actorList[item.ID];
        }
        else
        {
            return CreateActor(item);
        }
    }

    private MoodActor CreateActor(iActor item)
    {

#if UNITY_EDITOR || Development
        Debuger.Log("CreateActor:" + item.ID);
#endif
        var go = Loader.CreateObject(CFG.ActorPath + item.ID,transform as RectTransform);
        if (go)
        {
            var actor = go.GetComponent<MoodActor>();
            actor.ID = item.ID;
            actorList[actor.ID] = actor;
            return actor;
        }
        return null;
    }
}

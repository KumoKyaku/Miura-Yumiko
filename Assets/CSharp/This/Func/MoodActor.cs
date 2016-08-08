using UnityEngine;
using UnityEngine.UI;
using Poi;
using AVG;
using System;

public class MoodActor : MonoBehaviour, iLabel,iUseEffect
{
    private int _ID;
    [SerializeField]
    private string _Name;

    [SerializeField]
    private Image _Image;
    
    [SerializeField]
    private AudioSource _Sound;

    [SerializeField]
    private AudioClip[] _SoundClip;
    [SerializeField]
    private Sprite[] _SpriteMood;

    [SerializeField]
    private EffectPos effectPos;

    public Image Image
    {
        get
        {
            return _Image;
        }
    }

    public AudioSource Sound
    {
        get
        {
            return _Sound;
        }
    }

    public AudioClip[] SoundClip
    {
        get
        {
            return _SoundClip;
        }
    }

    public Sprite[] SpriteMood
    {
        get
        {
            return _SpriteMood;
        }
    }

    public int ID
    {
        get
        {
            return _ID;
        }
        set
        {
            _ID = value;
        }
    }

    public string Name
    {
        get
        {
            return _Name;
        }
        set
        {
            _Name = value;
        }
    }

    public EffectPos EffectPos
    {
        get
        {
            return effectPos;
        }
    }

    internal void Play(iActor item)
    {
        Image.sprite = FindSpiteMood(item.Mood);
        Sound.clip = FindAudioClip(item.Mood);
        SetTransform(item);
        gameObject.SetActive(true);
    }

    private void SetTransform(iActor item)
    {
        transform.localPosition = item.Position;
    }

    public AudioClip FindAudioClip(Mood _mood)
    {
        foreach (var item in SoundClip)
        {
            if (item.name.Contains(_mood.ToString()))
            {
                return item;
            }
        }
        return null;
    }

    public Sprite FindSpiteMood(Mood _mood)
    {
        foreach (var item in SpriteMood)
        {
            if (item.name.Contains(_mood.ToString()))
            {
                return item;
            }
        }
        return SpriteMood[0];
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

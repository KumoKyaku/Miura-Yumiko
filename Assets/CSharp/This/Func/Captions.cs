using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Poi;
using AVG;
using System;
using System.Collections.Generic;
using System.Linq;

public class Captions : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem _ParticleFront;
    [SerializeField]
    private ParticleSystem _Particleback;
    [SerializeField]
    private UnityEngine.UI.Text _Name;
    [SerializeField]
    private UnityEngine.UI.Text _Dialogue;
    [SerializeField]
    private Image _TextBG;
    [SerializeField]
    private AudioSource _DialogueSound;

    private int maxCountPerPage;

    public void Awake()
    {
        maxCountPerPage = CFG.MaxCountPerPage;
    }

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

    public UnityEngine.UI.Text Name
    {
        get
        {
            return _Name;
        }
    }

    public UnityEngine.UI.Text Dialogue
    {
        get
        {
            return _Dialogue;
        }
    }

    public Image TextBG
    {
        get
        {
            return _TextBG;
        }
    }
    int page;
    string[] stringList;
    static string[] stringarraynull = new string[0];
    private void Play(iSentence _sentence)
    {
        EffectManager.Play(_sentence.EffectList);
        page = 0;
        stringList = stringarraynull;
        switch (_sentence.SentenceType)
        {
            case SentenceType.Spoken:
                Name.text = Writing.Get(_sentence.SpeakerID);
                break;
            case SentenceType.Psychic:
                Name.text = Writing.Get(100034) + Writing.Get(_sentence.SpeakerID) + Writing.Get(100035);
                break;
            case SentenceType.Aside:
                break;
            case SentenceType.Option:
                break;
            case SentenceType.AllIn:
                gameObject.SetActive(false);
                return;
            default:
                break;
        }

        string _s = Writing.Get(_sentence.DialogueID);
        stringList = _s.SubPerCount(maxCountPerPage);
        Dialogue.text = System.Text.RegularExpressions.Regex.Unescape(stringList[page]); 
    }

    int subid = 0;
    Dictionary<int, iSentence> sentences;
    int[] keysort;
    public void Play(Dictionary<int, iSentence> sentence)
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
        subid = 0;
        sentences = sentence;
        keysort = sentences.Keys.OrderBy(id => id).ToArray();

        Play(sentences[keysort[subid]]);
    }
    public void Next()
    {
        if (stringList == null) return;
        if (page + 1 >= stringList.Length)
        {
            return;
        }
        else
        {
            page++;
            Dialogue.text = stringList[page];
        }
    }

    public bool NextPage()
    {
        if (page + 1 < stringList.Length)
        {
            Next();
            return true;
        }
        else if (keysort.Length> subid + 1)
        {
            subid++;
            Play(sentences[keysort[subid]]);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Last()
    {
        if (stringList == null) return;
        if (page <= 0)
        {
            if (subid > 0)
            {
                subid--;
                Play(sentences[keysort[subid]]);
                return;
            }
            else
            {
                GameManager.AVGPlayer.LastNode();
                return;
            }
        }
        else
        {
            page--;
            Dialogue.text = stringList[page];
        }
    }

    public AudioSource DialogueSound
    {
        get
        {
            return _DialogueSound;
        }
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

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Background : MonoBehaviour {

    [SerializeField]
    private ParticleSystem _Particle1;
    [SerializeField]
    private ParticleSystem _Particle2;
    [SerializeField]
    private Image _Image;
    [SerializeField]
    private Image _ImageTranslation;
    [SerializeField]
    private AudioSource _BGM;

    public ParticleSystem Particle1
    {
        get
        {
            return _Particle1;
        }
    }

    public ParticleSystem Particle2
    {
        get
        {
            return _Particle2;
        }
    }

    public Image Image
    {
        get
        {
            return _Image;
        }
    }

    public Image ImageTranslation
    {
        get
        {
            return _ImageTranslation;
        }
    }

    public AudioSource BGM
    {
        get
        {
            return _BGM;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;
using System.Xml.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Poi;
public class Func : MonoBehaviour//,IPointerClickHandler
{

    // Use this for initialization
    UnityEngine.UI.Text text;
	void Start () {
        //text = transform.FindChild("test").GetComponent<Text>();
        //StartCoroutine(AssetLoader.LoadAssetBundleManifest(CFG.WWWprefix + Application.streamingAssetsPath + "/AssetBundles/AssetBundles"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TestClick()
    {
        Debug.LogError("Click;");
    }

    public void Drag()
    {
        Debug.LogError("Drag is happend!!");
    }
    public void LoadXML()
    {
        StartCoroutine(LoadXML(""));
    }


    public IEnumerator LoadXML(string _name)
    {

#if UNITY_EDITOR
        Debug.Log(System.DateTime.Now);
#endif

        XElement textXML = null;
        string _path = "";
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        _path += "file:// ";
#elif UNITY_ANDROID
        //_path += "jar:file://";
#endif
        _path += Application.streamingAssetsPath + @"/Config.xml";
        //textXML = XElement.Load(_path);
        WWW www = new WWW(_path);
        while (!www.isDone)
        {
            yield return www;
            ///ParseXml(www);
        }

        ///var _node = textXML.FirstNode.ToString();

        textXML = XElement.Parse(www.text);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.LogError("!!Click");
    }

    
}

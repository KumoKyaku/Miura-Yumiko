using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Poi
{
    public class SystemUI : MonoBehaviour,ISystemUI
    {

        public UnityEngine.UI.Text Logger;
#if UNITY_EDITOR || Development
        bool oi = true;
#else
    bool oi = false;
#endif
        // Use this for initialization
        void Start() {
            UIManager.SystemUI = this;
            gameObject.SetActive(oi);
        }

        // Update is called once per frame
        void Update() {

        }

        public void Clear()
        {
            Logger.text = "";
        }

        public void Log(object _targer)
        {
            Logger.text += _targer.ToString();
        }

        public void OI(bool _oi)
        {
            gameObject.SetActive(_oi);
        }
    }
}
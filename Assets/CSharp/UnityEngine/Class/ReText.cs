using UnityEngine;
using System.Collections;
using System;

namespace Poi
{
    [RequireComponent(typeof(UnityEngine.UI.Text))]
    public class ReText : MonoBehaviour
    {
        private UnityEngine.UI.Text text;
        [SerializeField]
        private int textNum;
        private Language language;
        public int TextNum
        {
            get
            {
                return textNum;
            }
            set
            {
                textNum = value;
            }
        }

        // Use this for initialization
        void Start()
        {
            
            text = GetComponent<UnityEngine.UI.Text>();
            ReSetText();
        }

        void Update()
        {
            if (language != Writing.CurrentLanguage)
            {
                ReSetText();
            }
        }

        private void ReSetText()
        {
            text.text = Writing.Get(TextNum);
            language = Writing.CurrentLanguage;
        }
    }
}
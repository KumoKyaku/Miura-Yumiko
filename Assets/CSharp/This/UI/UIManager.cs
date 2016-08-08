using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Poi
{

    public class UIManager : aUIManager<UIManager>
    {

        [SerializeField]
        private Camera _UICamera;
        [SerializeField]
        private SystemUI _SystemUI;
        [SerializeField]
        private UI_Depth depth;

        //public static void ReSize()
        //{
        //    foreach (var item in UIList)
        //    {
        //        if (item.gameObject.activeInHierarchy)
        //        {
        //            item.ReUse();
        //        }
        //    }
        //}

        /// <summary>
        /// UI像机
        /// </summary>
        public static Camera UICamera
        {
            get
            {
                return Instance._UICamera;
            }
        }
        /// <summary>
        /// 系统UI，显示Log
        /// </summary>
        public static SystemUI SystemUI
        {
            get
            {
                return Instance._SystemUI;
            }

            set
            {
                Instance._SystemUI = value;
            }
        }

        public static Transform Transform { get { return Instance.transform; } }

        public static UI_Depth Depth { get { return Instance.depth; } }

        void Awake()
        {
            instance = this;
        }

        public static void Instantiate()
        {
            GameObject _go = Loader.CreateObject("Prepare/UIManager");
        }
    }
}

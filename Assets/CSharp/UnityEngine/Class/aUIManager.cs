using UnityEngine;
using System.Collections.Generic;
using System;

namespace Poi
{
    public abstract class aUIManager<M>:aManager<M>
    {


        static List<iUI> uiList = new List<iUI>();

        public static List<iUI> UIList
        {
            get
            {
                return uiList;
            }
        }

        /// <summary>
        /// 从UI列表中取得一个UI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_label"></param>
        /// <returns></returns>
        static public T GetUI<T>(iLabel _label = null)
            where T : iUI
        {
            if (_label == null)
            {
                foreach (var item in UIList)
                {
                    if (item.GetType() == typeof(T))
                    {
                        return (T)item;
                    }
                }
                return CreateUI<T>();
            }
            else
            {
                var _result = UIList.Find(_target => _target.ID == _label.ID || _target.Name == _label.Name);
                if (_result == null)
                {
                    return CreateUI<T>();
                }
                else
                {
                    return (T)_result;
                }
            }
        }

        /// <summary>
        /// 从UI列表中取得一个UI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_func"></param>
        /// <returns></returns>
        static public T GetUI<T>(Func<iUI, bool> _func)
            where T : iUI
        {
            foreach (var item in UIList)
            {
                if (_func(item))
                {
                    return (T)item;
                }
            }
            return CreateUI<T>();
        }

        static public iUI GetUI(string _name)
        {
            var _result = UIList.Find(_target => _target.Name == _name);
            if (_result == null)
            {
                return CreateUI(_name);
            }
            else
            {
                return _result;
            }
        }

        /// <summary>
        /// 将一个新构造的UI加入UI列表中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_UI"></param>
        static public void AddUI<T>(T _UI) where T : iUI
        {
            if (_UI != null && !UIList.Contains(_UI))
            {
                UIList.Add(_UI);
            }
        }

        /// <summary>
        /// 从UI列表中移除一个UI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_UI"></param>
        /// <returns></returns>
        static public bool RemoveUI<T>(T _UI) where T : iUI
        {
            return UIList.Remove(_UI);
        }

        /// <summary>
        /// 实例化一个UI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_path"></param>
        /// <returns></returns>
        static private T CreateUI<T>()
            where T : iUI
        {
            string _name = typeof(T).ToString();
            _name = _name.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries).Last();

            return (T)CreateUI(_name);
        }

        static private iUI CreateUI(string _name)
        {
            GameObject _go = Loader.CreateObject(CFG.UIPath + _name);
            if (_go == null)
            {
                return null;
            }
            iUI _ui = _go.GetComponent<iUI>();
            UIList.Add(_ui);
            return _ui;
        }
    }
}

using System;
using System.Xml.Linq;

namespace Poi
{
    /// <summary>
    /// 工具
    /// </summary>
    public class Util
    {
        /// <summary>
        /// 自动填充属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_cfg">配置xml</param>
        /// <param name="_instance">静态属性为null</param>
        /// <param name="_callback"></param>
        public static void AutoFullProperties<T>(XElement _cfg, T _instance, Action<XElement,T> _callback = null)
        {
            if (_callback == null)
            {
                var collection = typeof(T).GetProperties();
                foreach (var item in collection)
                {
                    XElement _temp = _cfg.Element(item.Name);
                    if (_temp == null)
                    {
                        continue;
                    }
                    try
                    {
                        if (item.CanWrite)
                        {
                            item.SetValue(_instance, Convert.ChangeType(_temp.Value, item.PropertyType), null);
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            else
            {
                _callback(_cfg,_instance);
            }
        }

        /// <summary>
        /// 把属性转成XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_instance"></param>
        /// <returns></returns>
        public static XElement SaveProperties<T>(T _instance,string name = null)
        {
            string _n = name;
            if (string.IsNullOrEmpty(_n))
            {
                _n = typeof(T).ToString().Split(new string[] { "."},StringSplitOptions.RemoveEmptyEntries).Last();
            }

            XElement _root = new XElement(_n);
            _root.Add(new XAttribute("TypeName", typeof(T).ToString()));

            var collection = typeof(T).GetProperties();
            foreach (var item in collection)
            {
                XElement _temp = new XElement(item.Name);
                var ob = item.GetValue(_instance, null);
                if (ob == null)
                {
                    continue;
                }
                _temp.Value = ob.ToString();
                _root.Add(_temp);
            }

            return _root;
        }
    }
}

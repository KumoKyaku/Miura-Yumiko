using System.Collections.Generic;
using System.Xml.Linq;

namespace System
{
    /// <summary>
    /// Poi库支持的扩展方法。
    /// Poi support extension methods.
    /// </summary>
    static public partial class Extension
    {
        #region Dictionary
        /// <summary>
        /// <para>为 枚举，string 的词典提供一个快速配置方法。</para>
        /// <para>用途：快速生成一个词典用作配置</para>
        /// </summary>
        /// <typeparam name="TEnum">表示一个枚举类型。如果不是枚举，则什么也不会发生。</typeparam>
        /// <param name="_dic">目标词典</param>
        /// <param name="_cfg">配置的XML</param>
        /// <param name="_callback">自定义处理XML的方法</param>
        static public void Config<TEnum>(this Dictionary<TEnum, string> _dic, XElement _cfg,Action<Dictionary<TEnum, string>,XElement> _callback = null)
            where TEnum : struct, IConvertible, IComparable
        {
            if (_callback == null)
            {
                if (typeof(TEnum).IsEnum)
                {
                    foreach (TEnum item in System.Enum.GetValues(typeof(TEnum)))
                    {
                        XElement _temp = _cfg.Element(item.ToString());
                        _dic[item] = _temp == null ? "" : _temp.Value;
                    }
                }
            }
            else
            {
                _callback(_dic, _cfg);
            }
        }
        #endregion

        #region 格式转换
        /// <summary>
        /// 使用int.Parse转换一个字符串，这个方法没有处理int.Parse的异常。
        /// Use the int.Parse to convert a string, this method does not handle int.Parse's exceptions.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static public int ToInt(this string str)
        {
            return int.Parse(str);
        }

        /// <summary>
        /// 使用double.Parse转换一个字符串，这个方法没有处理double.Parse的异常。
        /// Use the double.Parse to convert a string, this method does not handle double.Parse's exceptions.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static public double ToDouble(this string str) 
        {
            return double.Parse(str);
        }

        static public float ToFloat(this string str)
        {
            return float.Parse(str);
        }

        /// <summary>
        /// 使用double.Parse转换一个字符串，保留2位小数,这个方法没有处理double.Parse的异常。
        /// Use the double.Parse to convert a string,reserve 2 decimal places,this method does not handle double.Parse's exceptions.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static public double ToDoubleX_XX(this string str)
        {
            return Math.Round(double.Parse(str),2);
        }

        /// <summary>
        /// 如果对象为null返回false；否则返回true。
        /// If object is null return false,else return true.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        static public bool ToBool(this object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region String

        /// <summary>
        /// 按长度分割字符串
        /// </summary>
        /// <param name="_s"></param>
        /// <param name="PerCount"></param>
        /// <returns></returns>
        public static string[] SubPerCount(this string _s,int PerCount)
        {
            if (PerCount <= 0)
            {
                PerCount = 1;
            }

            if (_s.Length <= PerCount)
            {
                return new string[] { _s };
            }

            List<string> _temp = new List<string>();
            int i = 0;
            while (_s.Length - PerCount * i > PerCount)
            {
                _temp.Add(_s.Substring(PerCount * i, PerCount));
                i++;
            }
            _temp.Add(_s.Substring(PerCount * i, _s.Length - PerCount * i));

            return _temp.ToArray();
        }
        #endregion
        #region 数组第一个值和最后一个值

        static public T First<T>(this T[] _array)
        {
            if (_array.Length == 0)
            {
                return default(T);
            }
            else
            {
                return _array[0];
            }
        }

        static public T Last<T>(this T[] _array)
        {
            if (_array.Length == 0)
            {
                return default(T);
            }
            else
            {
                return _array[_array.Length -1];
            }
        }

        #endregion
    }
}
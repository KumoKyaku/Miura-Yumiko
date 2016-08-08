using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Poi
{
    /// <summary>
    /// <para>多语言文本查询。</para>
    /// <para>创建XML为每个text指定一个ID号并配置多种语言，方便翻译和扩展。</para>
    /// <para>初始化Text需要遵从示例的格式</para>
    /// <pare>使用CreatTemlate方法在指定路径下创建一个XML模版。文件名默认为：TranslatorText。</pare>
    /// <para>Interpreter</para>
    /// <para>XML specify an ID number for each text is created and configured in multiple languages to facilitate translation and expansion.</para>
    /// <para>Initializes the Text needs to follow the example format</para>
    /// <pare>Under the specified path using the CreatTemlate method to create an XML template. File name will default to: TranslatorText.</pare>
    /// </summary>
    public static class Writing
    {
        ///// <summary>
        ///// Text 实例；使用前初始化；
        ///// Instance.Initialization before use.
        ///// </summary>
        //static readonly public Text Instance = new Text();
        //private Text() { }

        static Language currentLanguage = Language.Chinese;

        /// <summary>
        /// 当前使用的语言
        /// </summary>
        public static Language CurrentLanguage
        {
            get { return currentLanguage; }
            set { currentLanguage = value; }
        }

        static Dictionary<int, string> text = new Dictionary<int, string>();

        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <param name="textXML">指定xml</param>
        /// <param name="language">设定从xml中加载的语言</param>
        public static void Init(XElement textXML, Language language)
        {
            string Lname = language.ToString();

            var collection = from node in textXML.Elements("Item")
                             where node.Attribute("ID") != null
                             select new
                             {
                                 ID = int.Parse(node.Attribute("ID").Value),
                                 Content = node.Attribute(Lname) == null ? null : node.Attribute(Lname).Value
                             };

            foreach (var item in collection)
            {
                if (item.Content == null)
                {
                    continue;
                }

                text[item.ID] = item.Content;
            }
        }

        /// <summary>
        /// 创建一个xml模版，初始化Text需要遵从这个示例的格式
        /// </summary>
        /// <param name="xmlPath">指定一个路径</param>
        /// <param name="fileName">指定文件名字，需要.xml后缀</param>
        public static void CreatTemlate(string xmlPath, string fileName = "TranslatorText.xml")
        {
            try
            {
                if (Directory.Exists(xmlPath) == false)
                {
                    Directory.CreateDirectory(xmlPath);
                }
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                //使用XDocument可能会更好；

                //初始化一个xml实例
                XmlDocument textXML = new XmlDocument();
                // 添加文档定义        
                textXML.AppendChild(textXML.CreateXmlDeclaration("1.0", "utf-8", "yes"));
                textXML.AppendChild(textXML.CreateComment("ID[800000-999999]默认用于配置表头！"));
                //创建xml的根节点
                XmlElement rootElement = textXML.CreateElement("Root");

                rootElement.SetAttribute("namespace","LanguageText");
                //将根节点加入到xml文件中（AppendChild）
                textXML.AppendChild(rootElement);

                //初始化第一层的第一个子节点
                XmlElement firstLevelElement1 = textXML.CreateElement("Item");

                //填充第一层的第一个子节点的属性值（SetAttribute）
                firstLevelElement1.SetAttribute("ID", "100000");

                //将第一层的第一个子节点加入到根节点下
                rootElement.AppendChild(firstLevelElement1);

                XmlElement firstLevelElement2 = textXML.CreateElement("Item");
                firstLevelElement2.SetAttribute("ID", "999999");

                rootElement.AppendChild(firstLevelElement2);


                foreach (Language item in Enum.GetValues(typeof(Language)))
                {
                    switch (item)
                    {
                        case Language.Chinese:
                            firstLevelElement1.SetAttribute(item.ToString(), "示例1");
                            firstLevelElement2.SetAttribute(item.ToString(), "示例2");
                            break;
                        case Language.English:
                            firstLevelElement1.SetAttribute(item.ToString(), "Example1");
                            firstLevelElement2.SetAttribute(item.ToString(), "Example2");
                            break;
                        case Language.Japanese:
                            firstLevelElement1.SetAttribute(item.ToString(), "例1");
                            firstLevelElement2.SetAttribute(item.ToString(), "例2");
                            break;
                        default:
                            firstLevelElement1.SetAttribute(item.ToString(), "10000000");
                            firstLevelElement2.SetAttribute(item.ToString(), "99999999");
                            break;
                    }
                }

                //将xml文件保存到指定的路径下
                textXML.Save(xmlPath + @"\"+fileName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        static public XElement AddItem(XElement _doc, int min,int max)
        {
            if (min > max)
            {
                return _doc;
            }
            XElement _root = _doc.Element("Root");
            var _item = from node in _doc.Elements()
                        where node.Attribute("ID") != null && node.Attribute("ID").Value.ToInt() >= min 
                        && node.Attribute("ID").Value.ToInt() <= max
                        select node.Attribute("ID").Value.ToInt();
            for (int i = min; i < max; i++)
            {
                if (_item.Contains(i))
                {
                    continue;
                }
                XElement _temp = new XElement("Item");
                _temp.SetAttributeValue("ID", i);
                if (_root == null)
                {
                    _doc.Add(_temp);
                }
                else
                {
                    _root.Add(_temp);
                }
            }
            return _doc;
        }

        ///// <summary>
        ///// 索引器
        ///// </summary>
        ///// <param name="textID">文本编号</param>
        ///// <returns>对应编号和设定语言的文本。如果文本编号不存在，返回[NotFoundText:编号]。</returns>
        //public string this[int textID]
        //{
        //    get
        //    {
        //        if (text.ContainsKey(textID))
        //        {
        //            return text[textID];
        //        }
        //        else
        //        {
        //            return "[NotFoundText:" + textID.ToString()+"]";
        //        }
        //    }
        //}

        public static string Get(int textID)
        {
            if (text.ContainsKey(textID))
            {
                return text[textID];
            }
            else
            {
                switch (CurrentLanguage)
                {
                    case Language.Chinese:
                        return "[没有找到文本:" + textID.ToString() + "]";
                    case Language.English:
                        return "[NotFoundText:" + textID.ToString() + "]";
                    default:
                        return "[没有找到文本:" + textID.ToString() + "]";
                }
            }
        }
    }
}

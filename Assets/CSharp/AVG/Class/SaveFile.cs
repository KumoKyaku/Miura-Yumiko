using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace AVG
{
    public class SaveFile
    {
        public bool IsFirst { get; internal set; }
        public int SceneID { get; internal set; }
        public int NodeID { get; internal set; }
        public bool Use5MaoEffect { get; internal set; }
        HashSet<int> key = new HashSet<int>();

        private int[] file = new int[10];

        public Dictionary<int, int> ChoiceResult
        {
            get
            {
                return choiceResult;
            }
        }

        public HashSet<int> Key
        {
            get
            {
                return key;
            }
        }

        public int[] File
        {
            get
            {
                return file;
            }
        }

        Dictionary<int, int> choiceResult = new Dictionary<int, int>();
        public SaveFile()
        {
            IsFirst = true;
        }

        public XElement ToXml()
        {
            XElement doc = new XElement("Save");

            var collection = this.GetType().GetProperties();
            foreach (var item in collection)
            {
                if (item.PropertyType == typeof(int) || item.PropertyType == typeof(string) || item.PropertyType == typeof(bool))
                {
                    XElement _temp = new XElement(item.Name);
                    var ob = item.GetValue(this, null);
                    if (ob == null)
                    {
                        continue;
                    }
                    _temp.Value = ob.ToString();
                    doc.Add(_temp);
                } 
            }

            XElement choice = new XElement("choiceResult");
            foreach (var item in choiceResult)
            {
                XElement _temp = new XElement("item");
                _temp.Add(new XAttribute("Key",item.Key));
                _temp.Add(new XAttribute("Value", item.Value));
                choice.Add(_temp);
            }

            XElement _f = new XElement("file");
                        
            for (int i = 0; i < file.Length; i++)
            {
                XElement _temp = new XElement("item");
                _temp.Add(new XAttribute("Key", i));
                _temp.Add(new XAttribute("Value", file[i]));
                _f.Add(_temp);
            }

            XElement _key = new XElement("Key");
            foreach (var item in key)
            {
                XElement _temp = new XElement("item");
                _temp.Value = item.ToString();
                _key.Add(_temp);
            }
            doc.Add(_key);
            doc.Add(choice);
            doc.Add(_f);
            return doc;
        }

        public void Init(XElement xElement)
        {
            var collection = this.GetType().GetProperties();
            foreach (var item in collection)
            {
                if (item.PropertyType == typeof(int) || item.PropertyType == typeof(string) || item.PropertyType == typeof(bool))
                {
                    XElement _temp = xElement.Element(item.Name);
                    if (_temp == null)
                    {
                        continue;
                    }
                    try
                    {
                        if (item.CanWrite)
                        {
                            item.SetValue(xElement, Convert.ChangeType(_temp.Value, item.PropertyType), null);
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }

            XElement choice = xElement.Element("choiceResult");
            if (choice != null)
            {
                var co = choice.Elements("item");
                foreach (var item in co)
                {
                    if (item.Attribute("Key") == null)
                    {
                        continue;
                    }
                    int key = item.Attribute("Key").Value.ToInt();
                    int value = item.Attribute("Value").Value.ToInt();
                    choiceResult[key] = value;
                }
            }

            XElement _f = xElement.Element("file");
            if (_f != null)
            {
                var co = _f.Elements("item");
                foreach (var item in co)
                {
                    if (item.Attribute("Key") == null)
                    {
                        continue;
                    }
                    int key = item.Attribute("Key").Value.ToInt();
                    int value = item.Attribute("Value").Value.ToInt();
                    file[key] = value;
                }
            }


            XElement _key = xElement.Element("Key");
            if (_key != null)
            {
                var co = _key.Elements("item");
                foreach (var item in co)
                {
                    key.Add(item.Value.ToInt());
                }
            }
        }
    }
}

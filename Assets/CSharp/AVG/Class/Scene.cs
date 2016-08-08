using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Poi;

namespace AVG
{
    /// <summary>
    /// 场景
    /// </summary>
    public class Scene : iScene
    {
        private string bgAudio;
        private string bgImage;
        private int iD;
        private string name;
        Dictionary<int, iNode> nodelist = new Dictionary<int, iNode>();
        public Scene(XElement _scene)
        {
            iD = _scene.Attribute("ID").Value.ToInt();
            name = _scene.Attribute("Name").Value;
            bgImage = _scene.Attribute("BGImage").Value;
            bgAudio = _scene.Attribute("BGAudio").Value;

            var collection = _scene.Elements("Node");
            foreach (var item in collection)
            {
                var _node = new Node(item, this);
                nodelist[_node.ID] = _node;
            }
        }

        #region iScene
        public string BGAudio
        {
            get
            {
                return bgAudio;
            }
        }

        public string BGImage
        {
            get
            {
                return bgImage;
            }
        }

        public int ID
        {
            get
            {
                return iD;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Dictionary<int, iNode> Nodelist
        {
            get
            {
                return nodelist;
            }
        }

        public bool ContainNode(int next)
        {
            return nodelist.ContainsKey(next);
        }



        #endregion
    }
}

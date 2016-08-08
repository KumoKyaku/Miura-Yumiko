using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Poi;

namespace AVG
{
    public class Node : iNode
    {
        private string changeBGImage;
        private string changeBGM;
        private int iD;
        private int next;
        private NodeType type;
        Dictionary<int, int> brunch = new Dictionary<int, int>();
        List<iActor> actors = new List<iActor>();
        Dictionary<int, iSentence> sentences = new Dictionary<int, iSentence>();
        private iScene owner;
        private int lastID;

        public Node(XElement item, iScene _s)
        {
            iD = item.Attribute("ID").Value.ToInt();
            Init(item, _s);
            
        }

        private void Init(XElement item, iScene scene)
        {
            owner = scene;
            type = (NodeType)Enum.Parse(typeof(NodeType), item.Attribute("NodeType").Value);
            #region NodeType
            //switch (item.Attribute("NodeType").Value)
            //{
            //    case "Sentence":
            //        type = NodeType.Sentence;
            //        break;
            //    case "0":
            //        type = NodeType.Sentence;
            //        break;
            //    case "句子":
            //        type = NodeType.Sentence;
            //        break;
            //    case "Brunch":
            //        type = NodeType.Brunch;
            //        break;
            //    case "1":
            //        type = NodeType.Brunch;
            //        break;
            //    case "分支":
            //        type = NodeType.Brunch;
            //        break;
            //    case "Option":
            //        type = NodeType.Option;
            //        break;
            //    case "2":
            //        type = NodeType.Option;
            //        break;
            //    case "选项":
            //        type = NodeType.Option;
            //        break;
            //    case "End":
            //        type = NodeType.End;
            //        break;
            //    case "3":
            //        type = NodeType.End;
            //        break;
            //    case "结局":
            //        type = NodeType.End;
            //        break;
            //    default:
            //        type = NodeType.Sentence;
            //        break;
            //}
            #endregion

            var _attribute = item.Attribute("ChangeBGImage");
            if (_attribute != null) changeBGImage = _attribute.Value;
            _attribute = item.Attribute("ChangeBGM");
            if (_attribute != null) changeBGM = _attribute.Value;
            _attribute = item.Attribute("Next");
            if (_attribute != null)
            {
                next = _attribute.Value.ToInt();
            }

            var collection = item.Elements("Brunch");
            foreach (var Brunchitem in collection)
            {
                brunch[Brunchitem.Attribute("SubID").Value.ToInt()] = Brunchitem.Attribute("Next").Value.ToInt();
            }

            collection = item.Elements("Sentence");
            foreach (var sentence in collection)
            {
                iSentence _sentence = new Sentence(sentence);
                sentences[_sentence.SubID] = _sentence;
            }

            collection = item.Elements("Actor");
            foreach (var actor in collection)
            {
                var _actor = new Actor(actor);
                actors.Add(_actor);
            }
        }

        public void SetLastID(int lastID)
        {
            this.lastID = lastID;
        }

        public IList<iActor> ActorList
        {
            get
            {
                return actors;
            }
        }

        public string ChangeBGImage
        {
            get
            {
                return changeBGImage;
            }
        }

        public string ChangeBGM
        {
            get
            {
                return changeBGM;
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

        public iNode NextNode
        {
            get
            {
                if (type == NodeType.Brunch)
                {
                    if (Libretto.Save.ChoiceResult.ContainsKey(next))
                    {
                        return Libretto.GetNode(brunch[Libretto.Save.ChoiceResult[next]]);
                    }
                    else
                    {
                        return Libretto.GetNode(brunch.First().Value);
                    }
                }
                return Libretto.GetNode(next);
            }
        }

        public iNode LastNode
        {
            get
            {
                return Libretto.GetNode(lastID);
            }
        }

        public iScene Scene
        {
            get
            {
                return owner;
            }
        }

        public NodeType Type
        {
            get
            {
                return type;
            }
        }

        public Dictionary<int, iSentence> Sentences
        {
            get
            {
                return sentences;
            }
        } 
    }
}

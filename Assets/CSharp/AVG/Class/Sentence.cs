using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Poi;
namespace AVG
{
    public class Sentence:iSentence
    {
        private int iD;
        private int result;
        private int speakerID;
        private SentenceType type;
        private int subID = 0;
        List<iEffect> effList = new List<iEffect>();
        public Sentence(XElement item)
        {
            iD = item.Attribute("ID").Value.ToInt();
            type = (SentenceType)Enum.Parse(typeof(SentenceType), item.Attribute("SentenceType").Value);

            speakerID = item.Attribute("SpeakerID").Value.ToInt();

            var _attribute = item.Attribute("SubID");
            if (_attribute != null) subID = _attribute.Value.ToInt();

            var collection = item.Elements("Effect");
            foreach (var eff in collection)
            {
                iEffect tar = new Effect(eff);
                effList.Add(tar);
            }
        }

        public int DialogueID
        {
            get
            {
                return iD;
            }
        }

        public SentenceType SentenceType
        {
            get
            {
                return type;
            }
        }

        public int SpeakerID
        {
            get
            {
                return speakerID;
            }
        }

        public int SubID
        {
            get
            {
                return subID;
            }
        }

        public IList<iEffect> EffectList
        {
            get
            {
                return effList;
            }
        }
    }
}
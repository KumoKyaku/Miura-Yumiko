using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Poi;
namespace AVG
{
    public interface iNode:iID
    {
        IList<iActor> ActorList { get; }
        string ChangeBGImage { get; }
        string ChangeBGM { get; }
        iNode NextNode { get; }
        iNode LastNode { get; }
        iScene Scene { get; }
        Dictionary<int, iSentence> Sentences { get; }
        NodeType Type { get; }
        void SetLastID(int lastID);
    }
}

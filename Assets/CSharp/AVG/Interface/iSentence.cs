using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AVG
{
    public interface iSentence
    {
        int DialogueID { get; }
        SentenceType SentenceType { get; }
        int SpeakerID { get; }
        int SubID { get; }
        IList<iEffect> EffectList { get; }
    }
}

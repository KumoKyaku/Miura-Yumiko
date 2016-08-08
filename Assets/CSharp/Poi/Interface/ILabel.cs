using System;

namespace Poi
{
    /// <summary>
    /// 标签，含有ID和Name
    /// </summary>
    public interface iLabel:iID,iName
    {
    }

    public class Label : iLabel
    {
        public int ID
        {
            get
            ;

            set
            ;
        }

        public string Name
        {
            get
            ;

            set
            ;
        }
    }
}

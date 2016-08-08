using System.Collections.Generic;
using Poi;
namespace AVG
{
    /// <summary>
    /// 场景接口
    /// </summary>
    public interface iScene:iLabel
    {
        string BGImage { get; }

        string BGAudio { get; }

        bool ContainNode(int next);
        Dictionary<int, iNode> Nodelist { get; }
    }
}

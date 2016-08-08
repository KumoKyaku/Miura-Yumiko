using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AVG
{
    /// <summary>
    /// 句子类型
    /// </summary>
    public enum SentenceType
    {
        /// <summary>
        /// 说出声的
        /// </summary>
        Spoken = 0,
        /// <summary>
        /// 心理活动
        /// </summary>
        Psychic = 1,
        /// <summary>
        /// 旁白
        /// </summary>
        Aside = 2,
        /// <summary>
        /// 选择文本
        /// </summary>
        Option = 3,
        ///只有背景图模式
        AllIn = 4,
    }

    /// <summary>
    /// 节点类型
    /// </summary>
    public enum NodeType
    {
        /// <summary>
        /// 句子
        /// </summary>
        Sentence = 0,
        /// <summary>
        /// 分支
        /// </summary>
        Brunch = 1,
        /// <summary>
        /// 选项
        /// </summary>
        Option = 2,
        /// <summary>
        /// 结局
        /// </summary>
        End = 3,
    }
}


using System;
using System.Xml.Linq;

namespace Poi
{
    /// <summary>
    /// 配置
    /// </summary>
    public class CFG
    {
        /// <summary>
        /// xml路径
        /// </summary>
        public static string PATH { get; set; }
        public static int FPS { get; internal set; }
        public static string UIPath { get; internal set; }
        public static string SpritePath { get; internal set; }
        public static string AudioClipPath { get; internal set; }
        public static int AutoGap { get; internal set; }
        public static int FirstNode { get; internal set; }
        public static int FirstScene { get; internal set; }
        public static string ActorPath { get; internal set; }
        public static int MaxCountPerPage { get; internal set; }
        public static string PreparePath { get; internal set; }
        public static int SaveCount { get; internal set; }


        #region WWWprefix
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        /// <summary>
        /// WWW加载资源的前缀
        /// </summary>
        public static readonly string WWWstreamingAssetsPathPrefix = "file:///";
#elif UNITY_ANDROID
        public static readonly string WWWstreamingAssetsPathPrefix ="";// "jar:file://";
#endif
        #endregion
        public static readonly string WWWpersistentDataPathPrefix = "file:///";
    }
}

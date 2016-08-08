using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Poi;
namespace AVG
{
    /// <summary>
    /// 剧本
    /// </summary>
    static public class Libretto
    {
        static Dictionary<int, iScene> scenes = new Dictionary<int, iScene>();
        static public iAVGPlayer Player { get; set; }
        /// <summary>
        /// 游戏的所有场景
        /// </summary>
        public static Dictionary<int, iScene> Scenes { get { return scenes; } }
        public static SaveFile Save { get { return GameManager.Save; } }
        /// <summary>
        /// 当前场景
        /// </summary>
        public static iScene CurrentScene { get; set; }

        /// <summary>
        /// 是否加载完成
        /// </summary>
        public static bool IsFinished { get; set; }
        public static iNode CurrentNode { get; private set; }

        /// <summary>
        /// 初始化剧本
        /// </summary>
        static public void Init(XElement _libretto)
        {
            new System.Threading.Thread(() => 
            {
                var collection = _libretto.Elements("Scene");
                foreach (var item in collection)
                {
                    InitScene(item);
                }
                ///关联节点父子关系
                foreach (var item in scenes)
                {
                    foreach (var node in item.Value.Nodelist)
                    {
                        var nodeNext =  node.Value.NextNode;
                        if (nodeNext == null)
                        {
                            continue;
                        }
                        else
                        {
                            nodeNext.SetLastID(node.Key);
                        }
                    }
                }
                IsFinished = true;
            }).Start();
        }

        public static iNode GetNode(int v)
        {
            if (CurrentScene != null && CurrentScene.ContainNode(v))
            {
                return CurrentScene.Nodelist[v];
            }
            else
            {
                foreach (var item in Scenes)
                {
                    if (item.Value == CurrentScene)
                    {
                        continue;
                    }
                    if (item.Value.ContainNode(v))
                    {
                        return item.Value.Nodelist[v];
                    }
                }

                return null;
            }
        }

        static Scene InitScene(XElement _scene)
        {
            Scene _s = new Scene(_scene);
            scenes[_s.ID] = _s;
            return _s;
        }

        private static void NoPlayer()
        {
            if (GameManager.AVGPlayer != null)
            {
                Player = GameManager.AVGPlayer;
            }
            else
            {
#if UNITY_EDITOR || Development
                Debuger.Log("NoPlayer");
#endif
            }
        }


        private static void PlayScene(iScene _scene)
        {
            if (Player == null) return;
            {
                NoPlayer();
            }
            Player.LoadScene(_scene);
            CurrentScene = _scene;
        } 

        public static void PlayNode(iNode _node)
        {
            if (IsFinished && Player != null &&  _node != null)
            {
                if (_node.Scene != CurrentScene)
                {
                    PlayScene(_node.Scene);
                }
                CurrentNode = _node;
                Player.Play(_node);
            }
            else if (Player != null)
            {
                Player.Loading();
            }
            else
            {
                ///没有播放器
                NoPlayer();
            }
        }

        public static void PlayNode(int _node)
        {
            PlayNode(GetNode(_node));
        }

        public static void NextNode()
        {
            switch (CurrentNode.Type)
            {
                case NodeType.Sentence:
                    NextNode(CurrentNode.NextNode);
                    break;
                case NodeType.Brunch:
                    NextNode(CurrentNode.NextNode);
                    break;
                case NodeType.Option:
                    NextNode(CurrentNode.NextNode);
                    break;
                case NodeType.End:
                    BrunchEnd(CurrentScene, CurrentNode);
                    break;
                default:
                    break;
            }
        }

        public static void LastNode()
        {
            switch (CurrentNode.Type)
            {
                case NodeType.Sentence:

                    iNode lastNode = CurrentNode.LastNode;
                    if (lastNode.Type != NodeType.Sentence)
                    {
                        return;
                    }
                    if (lastNode.Scene == CurrentScene)
                    {
                        PlayNode(lastNode);
                    }
                    else
                    {
                        PlayScene(lastNode.Scene);
                        PlayNode(lastNode);
                    }
                    break;
                case NodeType.Brunch:
                    break;
                case NodeType.Option:
                    break;
                case NodeType.End:
                    break;
                default:
                    break;
            }
        }

        private static void NextNode(iNode _next)
        {
            if (_next.Scene == CurrentScene)
            {
                PlayNode(_next);
            }
            else
            {
                PlayScene(_next.Scene);
                PlayNode(_next);
            }
        }



        /// <summary>
        /// 结局处理
        /// </summary>
        /// <param name="currentScene"></param>
        /// <param name="currentNode"></param>
        private static void BrunchEnd(iScene currentScene, iNode currentNode)
        {
            NextNode(currentNode);
        }
    }
}

using System;
using System.Collections;
using System.Xml.Linq;
using AVG;
using UnityEngine;
using System.IO;
using System.Text;

namespace Poi
{
    /// <summary>
    /// 
    /// </summary>
    public class GameManager : aManager<GameManager>
    {
        public static GameState GameState { get; private set; }
        public static bool SplashDone { get; internal set; }
        public static AVGPlayer AVGPlayer { get; internal set; }
        public static SaveFile Save { get; internal set; }
        public static Sprite SpBG { get; internal set; }

        #region 实例
        // Use this for initialization
        void Awake()
        {
            instance = this;
            GameState = GameState.Loading;
        }


        void Start()
        {
            Save = new SaveFile()
            {
                IsFirst = true,
                SceneID = 1,
                NodeID = 1,
                Use5MaoEffect = true,
            };

            StartCoroutine(Load());    
        }

        public static void SaveFile()
        {
            using (StreamWriter wr = new StreamWriter(
                Application.persistentDataPath + "/Config.xml", false, new UTF8Encoding(false)))
            {
                Util.SaveProperties<CFG>(null).Save(wr);
            }

            using (StreamWriter wr = new StreamWriter(
                Application.persistentDataPath + "/Save.xml", false, new UTF8Encoding(false)))
            {
                Save.ToXml().Save(wr);
            }
        }



        // Update is called once per frame
        void Update()
        {
            if (GameState == GameState.Prepared && SplashDone)
            {
                GameStart();
            }
        }

        #endregion

        /// <summary>
        /// 游戏推出
        /// </summary>
        /// <param name="v"></param>
        public static void Exit(int v)
        {
            switch (v)
            {
                case 0:
                    Application.Quit();
                    break;
                case 1:
                    UIManager.GetUI<UI_Ask>().Use(new UI_Ask_Context()
                    {
                        Message = Writing.Get(200001),
                        Callback = (_yn, _ask) =>
                        {
                            if (_yn)
                            {
                                SaveFile();
                                Exit(0);
                            }
                            _ask.UseDone();
                        }
                    });
                    break;
                default:
                    Application.Quit();
                    break;
            }
        }

        static void GameStart()
        {

            LoadLevel("Start");

             {
#if UNITY_EDITOR || Development
                 Debuger.Log("准备完成，开始游戏");
#endif
             }
        }

        public static void LoadLevel(string v,Action loadDone = null)
        {
            GameState = GameState.Changing;

            var result = Application.LoadLevelAsync(v);
            UIManager.GetUI<UI_LoadImage>().Use(new UI_LoadImage_Context() { Process = result });
            if (result.allowSceneActivation || result.isDone)
            {
                GameState = GameState.Running;
                if (loadDone!=null)
                {
                    loadDone();
                }
            }
        }

        static IEnumerator LoadConfig()
        {
            WWW www = new WWW(CFG.WWWstreamingAssetsPathPrefix + Application.streamingAssetsPath+"/Config.xml");
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                Util.AutoFullProperties<CFG>(XElement.Parse(www.text), null);
            }
            else
            {

#if UNITY_EDITOR || Development
                Debuger.Log(www.error);
#endif
            }

            
        }

        private IEnumerator Load()
        {
            yield return StartCoroutine(LoadConfig());
            yield return StartCoroutine(LoadPreference());
            yield return StartCoroutine(LoadText());
            StartCoroutine(LoadLibretto());
            LoadPlayerPrefs();
        }

        private IEnumerator LoadPreference()
        {
            WWW www = new WWW(CFG.WWWpersistentDataPathPrefix + Application.persistentDataPath + "/Save.xml");
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                Save.Init(XElement.Parse(www.text));
            }
            else
            {

#if UNITY_EDITOR || Development
                Debuger.Log(www.error);
#endif
            }

            if (Save.IsFirst)
            {
                yield break;
            }


            WWW www2 = new WWW(CFG.WWWpersistentDataPathPrefix + Application.persistentDataPath + "/Config.xml");
            yield return www2;
            if (string.IsNullOrEmpty(www2.error))
            {
                Util.AutoFullProperties<CFG>(XElement.Parse(www2.text), null);
            }
            else
            {

#if UNITY_EDITOR || Development
                Debuger.Log(www2.error);
#endif
            }
        }

        private IEnumerator LoadLibretto()
        {
            WWW www = new WWW(CFG.WWWstreamingAssetsPathPrefix + Application.streamingAssetsPath + "/Libretto.xml");
            while (!www.isDone)
            {
                yield return null;
            }

            AVG.Libretto.Init(XElement.Parse(www.text));
        }

        /// <summary>
        /// 加载文本
        /// </summary>
        /// <returns></returns>
        public static IEnumerator LoadText(Language tar = Language.Chinese)
        {
            WWW www = new WWW(CFG.WWWstreamingAssetsPathPrefix + Application.streamingAssetsPath + "/Text.xml");
            while (!www.isDone)
            {
                yield return null;
            }

            Writing.Init(XElement.Parse(www.text), tar);
            //Text.AddItem(XElement.Parse(www.text), 100101, 100130).Save(Application.streamingAssetsPath + "/Text.xml");
            Writing.CurrentLanguage = tar;
        }        

        static void LoadPlayerPrefs()
        {
            ConfigGame();
            GameState = GameState.Prepared;
        }

        static void ConfigGame()
        {
            Application.targetFrameRate = CFG.FPS;
        }

        public static void AVGStart(int nodeID)
        {
            Instance.StartCoroutine(Avgstart(nodeID));
        }

        private static IEnumerator Avgstart(int id)
        {
            while (AVGPlayer == null)
            {
                yield return null;
            }

            Libretto.PlayNode(id);
        }
    }
}

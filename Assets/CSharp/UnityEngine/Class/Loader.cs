using System;
using System.IO;
using System.Collections;
using Poi;

namespace UnityEngine
{
    public static class Loader
    {
        public static void CreateDirectory()
        {
            if (Directory.Exists(Application.streamingAssetsPath) == false)
            {
                Directory.CreateDirectory(Application.streamingAssetsPath);
            }

            string[] files = Directory.GetFiles(Application.streamingAssetsPath, "*.xml", SearchOption.AllDirectories);
        }

        static public GameObject CreateObject(string path, Transform parent = null, bool isResetTransform = false)
        {
            UnityEngine.Object resource = Resources.Load(path);

            if (resource != null)
            {
                GameObject obj = UnityEngine.Object.Instantiate(resource) as GameObject;
                if (parent != null && obj != null)
                {
                    obj.transform.SetParent(parent);

                    if (isResetTransform)
                    {
                        obj.transform.localPosition = Vector3.zero;
                        obj.transform.localRotation = Quaternion.identity;
                        obj.transform.localScale = Vector3.one;
                    }
                }

                return obj;
            }

            return null;
        }

        public static AudioClip AudioClip(string _name)
        {
            return Resources.Load<AudioClip>(CFG.AudioClipPath + _name);
        }

        public static Sprite Sprite(string _name)
        {
            Sprite _temp = Resources.Load<Sprite>(CFG.SpritePath + _name);
            return Resources.Load<Sprite>(CFG.SpritePath + _name);
        }

        public static T Load<T>(string _path) where T : UnityEngine.Object
        {
            return Resources.Load<T>(_path);
        }

        public static void UnloadUnusedAssets()
        {
            Resources.UnloadUnusedAssets();
        }


        public static IEnumerator LoadAssetBundle()
        {
            string url = Application.streamingAssetsPath + "/AssetBundles/";
            string path =CFG.WWWstreamingAssetsPathPrefix + url + "AssetBundles";
            WWW nwww = WWW.LoadFromCacheOrDownload(path, 0);
            yield return nwww;
            if (string.IsNullOrEmpty( nwww.error))
            {
                AssetBundle mab = nwww.assetBundle;
                UnityEngine.Object g0 = mab.LoadAsset("AssetBundleManifest");
                AssetBundleManifest main = g0 as AssetBundleManifest;
                mab.Unload(false);

                string[] dps = main.GetAllDependencies("Commen");
                AssetBundle[] temp = new AssetBundle[dps.Length];
                foreach (var item in dps)
                {
                    WWW durkl = WWW.LoadFromCacheOrDownload(item, main.GetAssetBundleHash(item));
                    yield return durkl;
                    temp[0] = durkl.assetBundle;
                }

                WWW tar = WWW.LoadFromCacheOrDownload(CFG.WWWstreamingAssetsPathPrefix+ url + "Commen", main.GetAssetBundleHash("Commen"),0);
                yield return tar;
                AssetBundle t = tar.assetBundle;
                Sprite _tt = t.LoadAsset<Sprite>("20");
                GameObject g = new GameObject();
                var r = g.AddComponent<SpriteRenderer>();
                r.sprite = _tt;
            }
            else
            {

#if UNITY_EDITOR || Development
                Debuger.Log(nwww.error);
#endif
            }
            
        }
    }
}

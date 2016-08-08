using UnityEngine;
using System.Collections;
using System;
namespace UnityEngine
{
    /// <summary>
    /// 资源加载器
    /// </summary>
    public class AssetLoader
    {
        static AssetBundleManifest assetBundleManifest;
        /// <summary>
        /// 预热
        /// <para>建议游戏开始的时候调用一次</para>
        /// </summary>
        /// <returns></returns>
        public static IEnumerator Preheat()
        {
            yield return null;
        }

        public static T Load<T>(string _path) where T : Object
        {
            return Resources.Load<T>(_path);
        }

        /// <summary>
        /// 加载主依赖关系，会常驻在内存中
        /// </summary>
        /// <returns></returns>
        private static IEnumerator LoadAssetBundleManifest(string _assetBundlePath,int _version = 0)
        {
            WWW www = WWW.LoadFromCacheOrDownload(_assetBundlePath, _version);
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                AssetBundle mainb = www.assetBundle;
                var manifest = mainb.LoadAsset("AssetBundleManifest") as AssetBundleManifest;

#if UNITY_EDITOR || Development
                if (manifest == null)
                {
                    Debuger.LogError("AssetBundleManifest == null");
                }
#endif
                assetBundleManifest = manifest ?? assetBundleManifest;
                mainb.Unload(false);
            }
            else
            {
#if UNITY_EDITOR || Development
                Debuger.LogError(www.error);
#endif
            }
        }

        public static IEnumerator LoadAssetBundle(string Fdirctory,string FassetBundleName)
        {
            string[] dps = assetBundleManifest.GetAllDependencies(FassetBundleName);
            AssetBundle[] temp = new AssetBundle[dps.Length];
            for (int i = 0; i < dps.Length; i++)
            {
                WWW www = WWW.LoadFromCacheOrDownload(dps[i], assetBundleManifest.GetAssetBundleHash(dps[i]));
                yield return www;
                temp[i] = www.assetBundle;
            }

            WWW tar = WWW.LoadFromCacheOrDownload(Fdirctory + FassetBundleName, assetBundleManifest.GetAssetBundleHash(FassetBundleName));
            yield return tar;
            AssetBundle t = tar.assetBundle;
        }
    }
}
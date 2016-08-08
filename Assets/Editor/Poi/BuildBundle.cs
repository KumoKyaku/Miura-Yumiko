using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
public class BuildBundle : Editor
{
    /// <summary>
    /// 将多个Object创建成资源包
    /// </summary>
    [MenuItem("Poi/Create AssetBundles All")]
	public static void CreateAssetBundlesAll()
    {
        ///清理缓存
        Caching.CleanCache();
        ///资源包的名字
        string _name = "Commen";
        ///设定输出目录
        string _path = Application.streamingAssetsPath + "/AssetBundles/";
        if (!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);
        }
        //_path += _name;
        ///拿到选中文件和子文件夹中的文件
        UnityEngine.Object[] SelectedAsset = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
        ///输出资源日志
        foreach (var item in SelectedAsset)
        {
            Debug.Log("生成的资源包中的资源名字：" + item);
            var _de = AssetDatabase.GetDependencies(new string[] { AssetDatabase.GetAssetPath(item) });
            foreach (var itesm in _de)
            {
                AssetImporter _im = AssetImporter.GetAtPath(itesm);
                _im.assetBundleName = _name;
                _im.SaveAndReimport();
            }
        }

        BuildPipeline.BuildAssetBundles(_path,BuildAssetBundleOptions.IgnoreTypeTreeChanges, BuildTarget.StandaloneWindows64);
        AssetDatabase.Refresh();
    }
}

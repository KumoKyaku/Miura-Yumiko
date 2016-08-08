using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using Poi;

public class Poi2:Editor
{
    [MenuItem("Poi/Test")]
    public static void Test()
    {
        Caching.CleanCache();
        /////资源包的名字
        //string _name = "BG_";
        //int i = 0;
        ////_path += _name;
        /////拿到选中文件和子文件夹中的文件
        UnityEngine.Object[] SelectedAsset = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
        GameObject go = null;
        foreach (var item in SelectedAsset)
        {
            go = Instantiate(item) as GameObject;
            var re = go.GetComponent<ReText>();
            if (re == null)
            {
                re = go.AddComponent<ReText>();
            }

            re.TextNum = 5;

            PrefabUtility.ReplacePrefab(go, item);
            DestroyImmediate(go);
        }
        //AssetDatabase.CreateAsset(go,Application.streamingAssetsPath +  "/T.prefab");
    }
}


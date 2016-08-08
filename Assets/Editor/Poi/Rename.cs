using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
 /// <summary>
    /// 将多个Object创建成资源包
    /// </summary>
    
public class Rename : Editor {


    [MenuItem("Poi/Raname")]
    public static void RaName()
    {
        Caching.CleanCache();
        ///资源包的名字
        string _name = "BG_";
        int i = 0;
        //_path += _name;
        ///拿到选中文件和子文件夹中的文件
        UnityEngine.Object[] SelectedAsset = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
        foreach (var item in SelectedAsset)
        {
            i++;
            item.name = _name + i;
            
        }
        AssetDatabase.Refresh();
    }
}

using UnityEngine;
using System.Collections;
using System.IO;
using AVG;
using Poi;

public class CGRule : Rule {

    public UnityEngine.UI.Image image;

    public override void Commit()
    {
        base.Commit();
        var sp = image.sprite;
        GameManager.SpBG = sp;
        Find.Set(sp);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityEngine
{
    public static partial class Extension
    {
        #region RectTransform

        public static void Resize(this RectTransform _trans,Anchor _anchor, Vector2 offsetMin, Vector2 offsetMax)
        {
            _trans.Resize(_anchor.Min,_anchor.Max,offsetMin,offsetMax);
        }

        public static void Resize(this RectTransform _trans,Vector2 anchormin, Vector2 anchormax
            , Vector2 offsetMin, Vector2 offsetMax)
        {
            _trans.anchorMin = anchormin;
            _trans.anchorMax = anchormax;
            _trans.offsetMin = offsetMin;
            _trans.offsetMax = offsetMax;
        }
        #endregion
    }
}

using UnityEngine;
using System.Collections;
namespace Poi
{
    public class UI_Depth : MonoBehaviour
    {

        [SerializeField]
        private RectTransform front;
        [SerializeField]
        private RectTransform back;
        [SerializeField]
        private RectTransform[] depth;

        public RectTransform Front
        {
            get
            {
                return front;
            }
        }

        public RectTransform Back
        {
            get
            {
                return back;
            }
        }

        public RectTransform this[int _num]
        {
            get
            {
                if (_num >= depth.Length)
                {
                    return Back;
                }
                else
                {
                    return depth[_num];
                }
            }
        }

    }
}
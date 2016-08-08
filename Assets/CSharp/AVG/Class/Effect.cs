using System;
using System.Xml.Linq;
using Poi;
using UnityEngine;

namespace AVG
{
    /// <summary>
    /// 特效
    /// </summary>
    public class Effect : iEffect
    {
        public Effect(XElement _scene)
        {
            XAttribute _a = null;

            _a = _scene.Attribute("Name");
            Name = _a == null ? "" : _a.Value;

            _a = _scene.Attribute("Directions");
            Directions = _a == null ? 0 : _a.Value.ToInt();

            _a = _scene.Attribute("Delay");
            Delay = _a == null ? 0 : _a.Value.ToDouble();

            _a = _scene.Attribute("Life");
            Life = _a == null ? 1 : _a.Value.ToDouble();

            _a = _scene.Attribute("LoopTimes");
            IsLoop = _a == null;
            LoopTimes = _a == null ? 0 : _a.Value.ToInt();

            _a = _scene.Attribute("Owner");
            Owner = _a == null ? 0 : _a.Value.ToInt();

            _a = _scene.Attribute("Size");
            float delta = _a == null ? 1 : _a.Value.ToFloat();
            Size = new Vector3(delta,delta, 1);

            _a = _scene.Attribute("Pos");
            Pos = _a == null ? 0 : _a.Value.ToInt();
        }
        /// <summary>
        /// 方向
        /// </summary>
        public int Directions { get;private set; }
        /// <summary>
        /// 延迟
        /// </summary>
        public double Delay { get;private set; }
        /// <summary>
        /// 生命周期
        /// </summary>
        public double Life { get;private set; }
        /// <summary>
        /// 是否循环
        /// </summary>
        public bool IsLoop { get;private set; }
        /// <summary>
        /// 循环次数
        /// </summary>
        public int LoopTimes { get;private set; }
        /// <summary>
        /// 特效名字
        /// </summary>
        public string Name { get;private set; }

        public int Owner { get; private set; }

        public int Pos { get; private set; }

        public Vector3 Size { get; private set; }
    }
}

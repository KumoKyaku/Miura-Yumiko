using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Poi
{
    public abstract class aManager<T>:MonoBehaviour
    {
        protected static T instance;
        public static T Instance { get { return instance; } }
    }
}

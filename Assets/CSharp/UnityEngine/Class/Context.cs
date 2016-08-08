using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poi
{
    /// <summary>
    /// 上下文
    /// </summary>
    public class Context : iContext
    {
        public object GetParameter(int _index)
        {
            if (parameters.Count > _index)
            {
                return parameters[_index];
            }
            return null;
        }

        protected IList<object> parameters = new List<object>();
    }
}

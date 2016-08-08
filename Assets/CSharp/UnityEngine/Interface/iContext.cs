using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poi
{
    public interface iContext
    {
        object GetParameter(int _index);
    }
}

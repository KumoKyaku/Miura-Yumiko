using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AVG
{
    public interface iAVGPlayer
    {
        void Play(iNode firstNode);
        void Loading();
        void LoadScene(iScene scene);
    }
}

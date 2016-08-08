using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poi;
using UnityEngine;

namespace AVG
{
    public interface iActor : iLabel
    {
        Mood Mood { get; }
        Vector3 Position { get; }
        Vector2 Size { get; }
    }
}

using UnityEngine;

namespace AVG
{
    public interface iEffect
    {
        double Delay { get; }
        int Directions { get; }
        bool IsLoop { get; }
        double Life { get; }
        string Name { get; }
        int Owner { get; }
        int Pos { get; }
        Vector3 Size { get; }
    }
}

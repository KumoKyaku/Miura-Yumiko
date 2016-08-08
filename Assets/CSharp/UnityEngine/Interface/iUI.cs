namespace Poi
{
    public interface iUI:iLabel
    {
        Only IsOnly { get; }

        void Use();

        UnityEngine.GameObject gameObject { get; }

        void UseDone();

        void ReUse();
    }
}
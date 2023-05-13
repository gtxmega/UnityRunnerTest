using GameTypes;

namespace Logics.Interactables
{
    public interface IInteractive
    {
        void OnCollideAt(Actor actor);
        void OnOverlapExit(Actor actor);
    }
}
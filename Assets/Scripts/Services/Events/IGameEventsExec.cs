using GameTypes;

namespace Services.Events
{
    public interface IGameEventsExec
    {
        void OnActorSpawned(Actor actor);
        void OnLevelEnded();
        void OnLevelInitialized();
        void OnLevelLoaded();
        void OnLevelStart();
        void OnPlayerReceivedPoints(int current, int added);
    }
}
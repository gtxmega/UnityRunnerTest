using GameTypes;
using System;

namespace Services.Events
{
    public class GameEvents : IGameEvents, IGameEventsExec
    {
        public event Action LevelLoaded;
        public event Action LevelInitialized;

        public event Action LevelStart;
        public event Action LevelEnded;

        public event Action<Actor> ActorSpawned;

        public event Action<int, int> PlayerReceivedPoints;

        public void OnLevelLoaded() => LevelLoaded?.Invoke();
        public void OnLevelInitialized() => LevelInitialized?.Invoke();

        public void OnLevelStart() => LevelStart?.Invoke();
        public void OnLevelEnded() => LevelEnded?.Invoke();

        public void OnActorSpawned(Actor actor) => ActorSpawned?.Invoke(actor);

        public void OnPlayerReceivedPoints(int current, int added) => PlayerReceivedPoints?.Invoke(current, added);
    }
}
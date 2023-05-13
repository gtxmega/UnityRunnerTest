using GameTypes;
using System;

namespace Services.Events
{
    public interface IGameEvents
    {
        event Action LevelInitialized;
        event Action LevelLoaded;
        event Action LevelStart;
        event Action<Actor> ActorSpawned;
        event Action<int, int> PlayerReceivedPoints;
        event Action LevelEnded;
    }
}
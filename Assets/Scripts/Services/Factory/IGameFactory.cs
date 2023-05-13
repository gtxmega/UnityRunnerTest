using GameTypes;
using UnityEngine;

namespace Services.Factory
{
    public interface IGameFactory
    {
        Actor CreateActor(Actor prefab, Vector3 position);
        Platform CreatePlatform(Platform prefab, Vector3 position);
    }
}
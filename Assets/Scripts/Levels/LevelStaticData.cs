using GameTypes;
using UnityEngine;

namespace Levels
{
    public class LevelStaticData : MonoBehaviour
    {
        public Actor PlayerMainActor => _playerMainActor;

        private Actor _playerMainActor;

        public void SetPlayerMainActor(Actor playerMainActor)
            => _playerMainActor = playerMainActor;
    }
}
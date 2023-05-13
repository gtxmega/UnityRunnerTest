using GameTypes;
using UnityEngine;

namespace Levels
{
    [CreateAssetMenu(menuName = "Game/Level constants", fileName = "LevelConstants")]
    public class LevelConstants : ScriptableObject
    {
        public int TargetFPS => _targetFPS;
        public string SpawnPointTag => _spawnPointTag;

        public Actor PlayerActorPrefab => _playerActorPrefab;
        public ETeam PlayerTeam => _playerTeam;

        public int WinPlatformsCount => _winPlatformsCount;

        [SerializeField] private int _targetFPS = 59;
        [SerializeField] private string _spawnPointTag;

        [Header("Player")]
        [SerializeField] private Actor _playerActorPrefab;
        [SerializeField] private ETeam _playerTeam;

        [Header("Level")]
        [SerializeField] private int _winPlatformsCount;
    }
}
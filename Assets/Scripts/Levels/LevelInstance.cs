using Levels.StateMachine;
using Levels.StateMachine.States;
using Logics.Score;
using Services.Events;
using Services.Factory;
using Services.GameInput;
using Services.Locator;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    public class LevelInstance : MonoBehaviour
    {
        [SerializeField] private LevelConstants _levelConstants;
        [SerializeField] private LevelStaticData _levelStaticData;
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private PlayerScore _playerScore;

        private IServiceLocator _services;
        private IGameEvents _gameEvents;
        private IGameEventsExec _gameEventsExec;

        private LevelStateMachine _levelStateMachine;

        private void Awake()
        {
            ApplyLevelSettings();

            CreateServices();
            InjectServicesInSceneObjects();

            CreateLevelStateMachine();

            _levelStateMachine.EnterIn<LoadLevelState>();
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void ApplyLevelSettings()
        {
            Application.targetFrameRate = _levelConstants.TargetFPS;
        }

        private void CreateServices()
        {
            _services = new ServiceLocator();


            GameEvents gameEvents = new GameEvents();
            _gameEvents = gameEvents;
            _gameEventsExec = gameEvents;

            _services.RegisterService<IGameEvents>(gameEvents);
            _services.RegisterService<IGameEventsExec>(gameEvents);
            _services.RegisterService<LevelConstants>(_levelConstants);
            _services.RegisterService<LevelStaticData>(_levelStaticData);
            _services.RegisterService<PlayerScore>(_playerScore);
            _services.RegisterService<IInputHandler>(_inputHandler);

            GameFactory gameFactory = new GameFactory(_services);
            _services.RegisterService<IGameFactory>(gameFactory);
        }

        private void InjectServicesInSceneObjects()
        {
            var dependencies = FindObjectsOfType<MonoBehaviour>();
            
            foreach (MonoBehaviour target in dependencies)
            {
                if (target.TryGetComponent<IInjectServices>(out var inject))
                    inject.InjectServices(_services);
            }
        }

        private void CreateLevelStateMachine()
        {
            _levelStateMachine = new LevelStateMachine();

            _levelStateMachine
                .AddLevelState(new LoadLevelState(_levelStateMachine, _services))
                .AddLevelState(new InitializeLevelState(_levelStateMachine, _services))
                .AddLevelState(new LoopLevelState(_levelStateMachine, _services));
        }
    }
}
using Game.Scripts.Data;
using Game.Scripts.Enums;
using UniRx;

namespace Game.Scripts.Core
{
    public class BootstrapState : IState
    {
        private readonly ScenesConfig _scenesConfig;
        private readonly ISceneLoader _sceneLoader;
        private readonly IFsm<EGameState> _fsm;
        
        private CompositeDisposable _lifetimeDisposable = new();
        public BootstrapState(IFsm<EGameState> fsm, ScenesConfig scenesConfig, ISceneLoader sceneLoader)
        {
            _scenesConfig = scenesConfig;
            _sceneLoader = sceneLoader;
            _fsm = fsm;
        }
        public void Enter()
        {
            _sceneLoader.Load(_scenesConfig.Bootstrap.name);
            
            _lifetimeDisposable = new CompositeDisposable();
            var arenaData = _scenesConfig.Arenas[0];
            
            _sceneLoader.OnSceneLoaded
                .Where( a => a.name == arenaData.name )
                .Subscribe( _ => OnSceneLoaded())
                .AddTo(_lifetimeDisposable); 
            
            LoadArenaScene();
        }
        public void Exit()
        {
            _lifetimeDisposable.Clear();
        }

        void LoadArenaScene()
        {
            var arenaData = _scenesConfig.Arenas[0]; //Change to special class that provides info about selected arena and hero
            _sceneLoader.Load(arenaData.name);
        }

        void OnSceneLoaded()
        {
            _lifetimeDisposable.Clear();
            _fsm.Enter(EGameState.GameLoop);
        }
        
        public void Dispose() {}
    }
}

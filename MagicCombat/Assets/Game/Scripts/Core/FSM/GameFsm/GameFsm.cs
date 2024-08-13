using Game.Scripts.Arena;
using Game.Scripts.Data;
using Game.Scripts.Enums;
using Game.Scripts.Factories;
using Zenject;

namespace Game.Scripts.Core
{
    public class GameFsm : FsmBase<EGameState>
    {
        [Inject] private readonly ISceneLoader _sceneLoader;
        [Inject] private readonly ScenesConfig _scenesConfig;
        [Inject] private readonly ArenaInfoProvider _arenaInfoProvider;
        [Inject] private readonly PrefabsConfig _prefabsConfig;
        [Inject] private readonly IUnitSpawner _unitSpawner;
        
        public override void Initialize()
        {
            AddState(EGameState.Bootstrap, new BootstrapState(this, _scenesConfig, _sceneLoader));
            AddState(EGameState.GameLoop, new GameLoopState(this, _arenaInfoProvider, _prefabsConfig, _unitSpawner));
            
            Enter(EGameState.Bootstrap);
        }
    }
}

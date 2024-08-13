using System;
using Cinemachine;
using Game.Scripts.Arena;
using Game.Scripts.Enums;
using Game.Scripts.Factories;
using UniRx;
using Zenject;

namespace Game.Scripts.Core
{
    public class CameraService : IInitializable, IDisposable
    {
        [Inject] private readonly IUnitSpawner _unitSpawner;
        [Inject] private readonly IFsm<EGameState> _gameFsm;
        [Inject] private CinemachineVirtualCamera _camera;
        [Inject] private ArenaInfoProvider _arenaInfoProvider;
        
        private CompositeDisposable _lifetimeDisposable = new();
        
        public void Initialize()
        {
            Observable
                .CombineLatest(_unitSpawner.Hero, _gameFsm.ActiveState, (hero, gameState) => hero != null && gameState == EGameState.GameLoop)
                .Where(x => x)
                .Subscribe(_ => RefreshCameraFollowTarget())
                .AddTo(_lifetimeDisposable);
        }
        
        public void Dispose()
        {
            _lifetimeDisposable.Dispose();
        }
        
        private void RefreshCameraFollowTarget()
        {
            _camera.Follow = _unitSpawner.Hero.Value.transform;
        }
    }
}

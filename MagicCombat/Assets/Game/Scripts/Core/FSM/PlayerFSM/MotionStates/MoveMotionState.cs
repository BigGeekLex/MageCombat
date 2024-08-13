using System;
using Game.Scripts.Enums;
using Game.Scripts.Units;
using UnityEngine;
using UniRx;

namespace Game.Scripts.Core
{
    public class MoveMotionState : IState, IPhysicsUpdatableState
    {
        private readonly IMovable _movable;
        private readonly IInputProvider _inputProvider;
        
        private readonly IFsm<EHeroMotionState> _fsm;
        private CompositeDisposable _lifetimeDisposable = new();
        private Vector2 _dir;
        public MoveMotionState(IFsm<EHeroMotionState> fsm, IMovable movable, IInputProvider inputProvider)
        {
            _movable = movable;
            _inputProvider = inputProvider;
            _fsm = fsm;
        }
        public void Enter()
        {
            _inputProvider
                .MoveDir
                .Subscribe(HandleMoveInput)
                .AddTo(_lifetimeDisposable);
        }
        public void Exit()
        {
            _lifetimeDisposable.Clear();
        }

        public void Dispose()
        {
            _lifetimeDisposable.Dispose();
        }
        
        public void UpdatePhysicsStateLogic()
        {
            _movable.Move(_dir);
        }
        
        void HandleMoveInput(Vector2 dir)
        {
            _dir = dir;
            
            if (dir == Vector2.zero)
                _fsm.Enter(EHeroMotionState.Idle);
        }
    }
}

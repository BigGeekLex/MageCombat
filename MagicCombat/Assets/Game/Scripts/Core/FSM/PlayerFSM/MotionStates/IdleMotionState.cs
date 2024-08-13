using Game.Scripts.Enums;
using UnityEngine;

namespace Game.Scripts.Core
{
    public class IdleMotionState : IState, IUpdatableState
    { 
        private readonly IInputProvider _inputProvider;

        private readonly IFsm<EHeroMotionState> _fsm;
        public IdleMotionState(IFsm<EHeroMotionState> fsm, IInputProvider inputProvider)
        {
            _inputProvider = inputProvider;
            _fsm = fsm;
        }
        
        public void Enter() {}
        
        public void Exit() {}
        
        public void UpdateStateLogic()
        {
            if (_inputProvider.MoveDir.Value != Vector2.zero) 
                _fsm.Enter(EHeroMotionState.Move);
        }
        
        public void Dispose() {}
    }
}

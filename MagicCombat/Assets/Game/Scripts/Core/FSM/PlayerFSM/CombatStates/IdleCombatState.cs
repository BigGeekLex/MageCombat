using Game.Scripts.Enums;
using Game.Scripts.Units;
using UniRx;
namespace Game.Scripts.Core
{
    public class IdleCombatState : IState
    {
        private readonly IInputProvider _inputProvider;
        private readonly IFsm<EHeroCombatState> _fsm;
        private CompositeDisposable _lifetimeDisposable = new();
        public IdleCombatState(IInputProvider inputProvider, IFsm<EHeroCombatState> fsm)
        {
            _inputProvider = inputProvider;
            _fsm = fsm;
            
            _inputProvider
                .OnAttack
                .Subscribe(_ => _fsm.Enter(EHeroCombatState.Attack))
                .AddTo(_lifetimeDisposable);
        }
        
        public void Enter() {}
        public void Exit() {}
        
        public void Dispose()
        {
            _lifetimeDisposable.Dispose();
        }
    }
}

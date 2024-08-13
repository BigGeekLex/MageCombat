using Game.Scripts.Enums;
using Game.Scripts.Units;
using UniRx;
using UnityEngine;

namespace Game.Scripts.Core
{
    public class CombatState : IState
    {
        private readonly ISpellLogic _spellLogic;
        private readonly IInputProvider _inputProvider;
        private readonly IFsm<EHeroCombatState> _fsm;
        private readonly UnitFacade _unitFacade;
        
        private CompositeDisposable _lifetimeDisposable = new();
        
        private int _spellIndex;
        public CombatState(ISpellLogic spellLogic, IInputProvider inputProvider, UnitFacade unit, IFsm<EHeroCombatState> fsm)
        {
            _spellLogic = spellLogic;
            _inputProvider = inputProvider;
            _fsm = fsm;
            _unitFacade = unit;

            _inputProvider.OnSwitchSpell
                .Subscribe(ChangeSpellIndex)
                .AddTo(_lifetimeDisposable);
        }
        
        public void Enter()
        {
            Vector2 dir = _unitFacade.transform.right;
            Vector2 moveDir = _inputProvider.MoveDir.Value;

            if (moveDir != Vector2.zero)
                dir = moveDir;
            
            _spellLogic.CastSpell(_spellIndex, dir);
            _fsm.Enter(EHeroCombatState.Idle);
        }
        
        public void Exit() {}

        private void ChangeSpellIndex(int delta)
        {
            _spellIndex = Mathf.Clamp(_spellIndex + delta, 0, _spellLogic.TotalSpells - 1);
        }
        
        public void Dispose()
        {
            _lifetimeDisposable.Dispose();
        }
    }
}

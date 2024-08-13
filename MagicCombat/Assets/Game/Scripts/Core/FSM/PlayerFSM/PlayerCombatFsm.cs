using Game.Scripts.Enums;
using Game.Scripts.Units;
using Zenject;

namespace Game.Scripts.Core
{
    public class PlayerCombatFsm : FsmBase<EHeroCombatState>
    {
        [Inject] private readonly ISpellLogic _spellLogic;
        [Inject] private readonly IInputProvider _inputProvider;
        [Inject] private readonly UnitFacade _unitFacade;
        
        public override void Initialize()
        {
            AddState(EHeroCombatState.Idle, new IdleCombatState(_inputProvider, this));
            AddState(EHeroCombatState.Attack, new CombatState(_spellLogic, _inputProvider, _unitFacade, this));
            Enter(EHeroCombatState.Idle);
        }
    }
}

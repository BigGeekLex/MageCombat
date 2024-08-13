using Game.Scripts.Enums;
using Game.Scripts.Units;
using UnityEngine.AI;
using Zenject;

namespace Game.Scripts.Core
{
    public class BotFsm : FsmBase<EBotState>
    {
        [Inject] private readonly ITargetFinder _targetFinder;
        [Inject] private readonly NavMeshAgent _agent;
        [Inject] private readonly UnitFacade _unitFacade;
        
        public override void Initialize()
        {
            AddState(EBotState.Attack, new MoveTargetBotState(_agent, _unitFacade, _targetFinder));
            Enter(EBotState.Attack);
        }
    }
}

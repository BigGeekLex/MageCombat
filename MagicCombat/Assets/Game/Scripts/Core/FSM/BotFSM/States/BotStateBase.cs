using Game.Scripts.Units;
using UnityEngine.AI;

namespace Game.Scripts.Core
{
    public abstract class BotStateBase : IState
    {
        protected readonly NavMeshAgent _agent;
        protected readonly UnitFacade _unitFacade;
        
        public BotStateBase(NavMeshAgent agent, UnitFacade unitFacade)
        {
            _agent = agent;
            _unitFacade = unitFacade;
        }
        
        public abstract void Enter();
        public abstract void Exit();
        
        public virtual void Dispose(){}
    }
}

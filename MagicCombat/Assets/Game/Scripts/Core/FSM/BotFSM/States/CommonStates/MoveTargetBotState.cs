using Game.Scripts.Enums;
using Game.Scripts.Units;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Scripts.Core
{
    public class MoveTargetBotState : BotStateBase, IUpdatableState
    {
        private readonly ITargetFinder _targetFinder;
        public MoveTargetBotState(NavMeshAgent agent, UnitFacade unit, ITargetFinder targetFinder) : base(agent, unit)
        {
            _targetFinder = targetFinder;
        }
        
        public override void Enter()
        {
            _agent.updateRotation = false;
        }

        public override void Exit()
        {
            _agent.ResetPath();   
        }
        
        public void UpdateStateLogic()
        {
            if (_unitFacade == null)
                return;
            
            RefreshTargetPosition();
            Rotate(_agent.velocity.normalized);
        }

        private void RefreshTargetPosition()
        {
            Vector3 pos = _unitFacade.gameObject.transform.position;
            UnitFacade target = _targetFinder.GetNearestTarget(pos, 120, EUnitCategory.Mage, null);
            Vector3 destination = target.transform.position;

            _agent.SetDestination(destination);
        }
        
        private void Rotate(Vector2 dir)
        {
            if (dir.x == 0) return;

            float angle = dir.x > 0 ? 0 : 180;
            _agent.transform.rotation = Quaternion.Euler(0,  angle, 0 );
        }
    }
}

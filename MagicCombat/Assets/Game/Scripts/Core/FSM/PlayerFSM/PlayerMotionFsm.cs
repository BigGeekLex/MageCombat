using Game.Scripts.Enums;
using Game.Scripts.Units;
using Zenject;

namespace Game.Scripts.Core
{
    public class PlayerMotionFsm : FsmBase<EHeroMotionState>
    {
        [Inject] private readonly IInputProvider _inputProvider;
        [Inject] private readonly IMovable _movable;
        
        public override void Initialize()
        {
            AddState(EHeroMotionState.Idle, new IdleMotionState(this, _inputProvider));
            AddState(EHeroMotionState.Move, new MoveMotionState(this, _movable, _inputProvider));
            Enter(EHeroMotionState.Idle);
        }
    }
}

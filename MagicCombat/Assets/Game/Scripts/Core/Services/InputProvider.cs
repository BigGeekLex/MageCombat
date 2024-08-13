using System;
using Game.Scripts.Extensions;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Core
{
    public interface IInputProvider
    {
        ReactiveCommand<int> OnSwitchSpell { get; }
        ReactiveCommand OnAttack { get; } 
        ReadOnlyReactiveProperty<Vector2> MoveDir { get; }
    }
    
    public class InputProvider : IInitializable, ITickable, IInputProvider, IDisposable
    {
        [Inject] private readonly IInputSystem _input;

        private ReactiveProperty<Vector2> _moveDir = new();
        private CompositeDisposable _lifetimeDis = new();
        public ReactiveCommand OnAttack { get; } = new();
        public ReactiveCommand<int> OnSwitchSpell { get; } = new();
        public ReadOnlyReactiveProperty<Vector2> MoveDir => _moveDir.ToReadOnlyReactiveProperty();

        public void Initialize()
        {
            _moveDir.Value = Vector2.zero;
            
            _input.Player.Spell1
                .PerformedAsObservable()
                .Subscribe( _ => OnSwitchSpell.Execute(-1))
                .AddTo(_lifetimeDis);

            _input.Player.Spell2
                .PerformedAsObservable()
                .Subscribe(_ => OnSwitchSpell.Execute(1))
                .AddTo(_lifetimeDis);
            
            _input.Player.Attack
                .StartedAsObservable()
                .Subscribe(_ =>  OnAttack.Execute())
                .AddTo(_lifetimeDis);
        }

        public void Tick()
        {
            HandleMoveInput(); 
        }

        public void Dispose()
        {
            _lifetimeDis.Dispose();
        }
        
        private void HandleMoveInput()
        {
            Vector2 moveDir = _input.Player.Move.ReadValue<Vector2>();
            _moveDir.Value = moveDir;
        }
    }
}

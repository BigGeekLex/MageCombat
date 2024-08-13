using System;
using System.Collections.Generic;
using UniRx;
using Zenject;

namespace Game.Scripts.Core
{
    public interface IFsm<T>
    {
        void Enter(T state);
        ReadOnlyReactiveProperty<T> ActiveState {get;}
    }
    
    public abstract class FsmBase<T> : IFsm<T>, ITickable, IFixedTickable, IInitializable, IDisposable
    {
        private Dictionary<T, IState> _states = new(); 
        private ReactiveProperty<T> _state = new();
        private IState _currentState;
        private IUpdatableState _updatableState;
        private IPhysicsUpdatableState _physicsUpdatableState;
        public ReadOnlyReactiveProperty<T> ActiveState {get;}

        public abstract void Initialize();
        
        public FsmBase()
        {
            ActiveState = _state.ToReadOnlyReactiveProperty();
        }
        
        public void AddState(T stateKey, IState state)
        {
            _states ??= new Dictionary<T, IState>();
            _states[stateKey] = state;
        }
        
        public void Enter(T state)
        {
            if (!_states.ContainsKey(state))
                throw new KeyNotFoundException($"State {state} not found");
            
            _currentState?.Exit();
            
            _currentState = _states[state];
            _state.Value = state;
            _updatableState = _currentState as IUpdatableState;
            _physicsUpdatableState = _currentState as IPhysicsUpdatableState;
            
            _currentState.Enter();
        }
        
        public void Tick() => _updatableState?.UpdateStateLogic();

        public void FixedTick() => _physicsUpdatableState?.UpdatePhysicsStateLogic();
        
        public void Dispose()
        {
            foreach (var state in _states)
            {
                state.Value.Dispose();
            }
        }
    }
}

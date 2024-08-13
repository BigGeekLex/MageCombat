using System;
namespace Game.Scripts.Core
{
    public interface IState : IDisposable
    {
        void Enter();
        void Exit();
    }

}

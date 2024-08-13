using Zenject;

namespace Game.Scripts.Core
{
    public interface IInputSystem
    {
        Controls.PlayerActions Player { get; }
    }
    
    public class InputSystem : IInitializable, IInputSystem
    {
        public Controls.PlayerActions Player { get; private set; }
        
        public void Initialize()
        {
            Controls controls = new Controls();
            Player = controls.Player;
            
            Player.Enable();
        }
    }
}

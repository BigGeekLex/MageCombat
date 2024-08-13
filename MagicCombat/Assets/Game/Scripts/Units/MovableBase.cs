using UnityEngine;

namespace Game.Scripts.Units
{
    public abstract class MovableBase : IMovable
    {
        public abstract void Move(Vector2 dir);
    }
}

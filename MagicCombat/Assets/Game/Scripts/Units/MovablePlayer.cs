using Game.Scripts.Arena;
using Game.Scripts.Enums;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Units
{
    public class MovablePlayer : MovableBase
    {
        [Inject] private readonly UnitModel _unitModel;
        [Inject] private readonly Rigidbody2D _characterRb;
        [Inject] private readonly Transform _transform;
        [Inject] private readonly ArenaInfoProvider _arenaInfoProvider;

        public override void Move(Vector2 dir)
        {
            float speed = _unitModel.MaxStats[EStat.Speed];
            Vector2 currentPos = _characterRb.position;
            Vector2 destination = currentPos + dir * speed * Time.fixedDeltaTime;
            
            float maxX = _arenaInfoProvider.ArenaSize.x / 2;
            float minX = -maxX;
            float maxY = _arenaInfoProvider.ArenaSize.y / 2;
            float minY = -maxY;
            float clampedX = Mathf.Clamp(destination.x, minX, maxX);
            float clampedY = Mathf.Clamp(destination.y, minY, maxY);
            
            
            Vector2 clampedDestination = new Vector2(clampedX, clampedY);
            _characterRb.MovePosition(clampedDestination);
            
            Rotate(dir);
        }
        
        private void Rotate(Vector2 dir)
        {
            if (dir.x == 0) return;

            float angle = dir.x > 0 ? 0 : 180;
            _transform.rotation = Quaternion.Euler(0,  angle, 0 );
        }
    }
}

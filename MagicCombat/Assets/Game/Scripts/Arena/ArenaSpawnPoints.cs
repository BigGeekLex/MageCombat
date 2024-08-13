using System.Linq;
using UnityEngine;


namespace Game.Scripts.Arena
{
    public class ArenaSpawnPoints : MonoBehaviour
    {
        [SerializeField] private Transform[] _enemySpawnPoints;
        [SerializeField] private Transform _playerSpawnPoint;

        public Vector2[] EnemySpawnPoints()
        {
            return _enemySpawnPoints.Select(t => new Vector2(t.position.x, t.position.y)).ToArray(); 
        }
        
        public Vector2 PlayerSpawnPoint()
        {
            return new Vector2(_playerSpawnPoint.position.x, _playerSpawnPoint.position.y);
        }
    }
}

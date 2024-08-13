using Game.Scripts.Data;
using UnityEngine;

namespace Game.Scripts.Arena
{
    public class ArenaInfoProvider
    {
        public Vector2 ArenaSize => ArenaConfig.Size;
        public ArenaSpawnPoints ArenaSpawnPoints { get; private set; }
        
        public ArenaConfig ArenaConfig { get; private set; }

        public void InitializeArena(ArenaSpawnPoints spawnPoints, ArenaConfig arenaConfig)
        {
            ArenaSpawnPoints = spawnPoints;
            ArenaConfig = arenaConfig;
        }
    }
}

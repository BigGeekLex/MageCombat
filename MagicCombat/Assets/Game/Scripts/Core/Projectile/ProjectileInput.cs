using Game.Scripts.Data;
using Game.Scripts.Units;
using UnityEngine;

namespace Game.Scripts.Core
{
    public struct ProjectileInput
    {
        public Vector2 Direction;
        public Vector2 SpawnPosition;
        public UnitFacade SourceCaster;
        public SpellConfig SpellConfig;

        public ProjectileInput(Vector2 direction, Vector2 spawnPosition, UnitFacade sourceCaster, SpellConfig spellConfig)
        {
            Direction = direction;
            SpawnPosition = spawnPosition;
            SourceCaster = sourceCaster;
            SpellConfig = spellConfig;
        }
    }
}

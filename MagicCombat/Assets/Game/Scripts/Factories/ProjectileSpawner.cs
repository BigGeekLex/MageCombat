using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Core;
using Game.Scripts.Enums;

namespace Game.Scripts.Factories
{
    public interface IProjectileSpawner
    {
        void InitProjectilePools(List<ProjectileBase.Pool> pools);
        void Spawn(ProjectileInput projectileInput);
    }
    
    public class ProjectileSpawner : IProjectileSpawner
    {
        private List<ProjectileBase.Pool> _projectilePools;
        
        public void InitProjectilePools(List<ProjectileBase.Pool> pools)
        {
            _projectilePools = pools;
        }
        
        public void Spawn(ProjectileInput pi)
        {
            var pool = FindPool(pi.SpellConfig.Spell); 
            ProjectileBase.Pool.Args args = new ProjectileBase.Pool.Args
            {
                Position = pi.SourceCaster.ProjectileSpawnPosition,
                Direction = pi.Direction,
                SourceCaster = pi.SourceCaster,
                TargetSpell = pi.SpellConfig
            };
            
            pool.Spawn(args);
        }
        
        private ProjectileBase.Pool FindPool(ESpell projectileId)
        {
            var pool = _projectilePools.FirstOrDefault(p => p.ProjectileId == projectileId);
            
            if (pool == null)
                throw new ArgumentException( $"Projectile pool with id {projectileId} not found." );
            
            return pool;
        }
    }
}

using System.Collections.Generic;
using Game.Scripts.Core;
using Game.Scripts.Factories;
using Zenject;

namespace Game.Scripts.Installers
{
    public class ArenaProjectilePoolResolver : IInitializable
    {
        private readonly IProjectileSpawner _projectileSpawner;
        private readonly List<ProjectileBase.Pool> _projectilePools;

        public ArenaProjectilePoolResolver(IProjectileSpawner projectileSpawner, 
            [Inject(Source = InjectSources.Local, Optional = true)] 
            List<ProjectileBase.Pool> projectilePools)
        { 
            _projectileSpawner = projectileSpawner; 
            _projectilePools = projectilePools;
        }

        public void Initialize()
        {
            _projectileSpawner.InitProjectilePools(_projectilePools);
        }
    }
}

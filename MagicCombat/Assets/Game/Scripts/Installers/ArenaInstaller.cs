using Cinemachine;
using Game.Scripts.Arena;
using Game.Scripts.Core;
using Game.Scripts.Data;
using Game.Scripts.Enums;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class ArenaInstaller : MonoInstaller
    {
        [SerializeField] private ArenaConfig _arenaConfig;
        [SerializeField] private Transform _allPoolsParent;
        
        private const int ProjectilePoolSize = 3;
        
        [Inject] private readonly PrefabsConfig _prefabsConfig;
        public override void InstallBindings()
        {
            Container
                .Bind<ArenaSpawnPoints>()
                .FromComponentsInHierarchy()
                .AsSingle();

            Container
                .Bind<ArenaConfig>()
                .FromInstance(_arenaConfig)
                .AsSingle();
            
            Container
                .Bind<CinemachineVirtualCamera>()
                .FromComponentsInHierarchy()
                .AsSingle();
            
            Container
                .BindInterfacesTo<CameraService>()
                .AsSingle();
            
            InstallPools();
        }

        private void InstallPools()
        {
            foreach (var projectileData in _prefabsConfig.Projectiles)
            {
                ESpell projectileId = projectileData.Spell;
                ProjectileBase prefab = projectileData.Projectile;
                Transform parent = CreatePoolParent(projectileId.ToString());
                
                Container
                    .BindMemoryPool<ProjectileBase, ProjectileBase.Pool>()
                    .WithInitialSize(ProjectilePoolSize)
                    .ExpandByOneAtATime()
                    .WithFactoryArguments(projectileId)
                    .FromComponentInNewPrefab(prefab)
                    .UnderTransform(parent);   
            }
        }
        
        private Transform CreatePoolParent(string poolName)
        {
            Transform parent = new GameObject(poolName).transform;

            parent.SetParent(_allPoolsParent);
            
            return parent;
        }
    }

}

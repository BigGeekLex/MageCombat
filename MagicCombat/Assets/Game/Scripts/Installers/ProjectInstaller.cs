using Game.Scripts.Arena;
using Game.Scripts.Data;
using Game.Scripts.Core;
using Game.Scripts.Factories;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private RootConfig _rootConfig;
        public override void InstallBindings()
        {
            InstallConfigs();
            InstallServices();
        }
        
        private void InstallServices()
        {
            Container.
                BindInterfacesTo<SceneLoader>()
                .FromInstance(_sceneLoader)
                .AsSingle();
            
            Container
                .BindInterfacesTo<InputSystem>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<InputProvider>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<DamageProcessor>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<TargetFinder>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<ArenaInfoProvider>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<ArenaInfoResolver>()
                .AsSingle()
                .CopyIntoDirectSubContainers();

           InstallFactories();
            
            Container
                .BindInterfacesTo<GameFsm>()
                .AsSingle();
        }

        private void InstallFactories()
        {
            Container
                .BindInterfacesTo<UnitSpawner>()
                .AsSingle();

            Container
                .BindInterfacesTo<ProjectileSpawner>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<ArenaProjectilePoolResolver>()
                .AsSingle()
                .CopyIntoDirectSubContainers();
        }

        private void InstallConfigs()
        {
            Container
                .BindInstance(_rootConfig.scenesConfig)
                .AsSingle();
            
            Container
                .BindInstance(_rootConfig.prefabsConfig)
                .AsSingle();
        }
    }
}

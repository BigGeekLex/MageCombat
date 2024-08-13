using Game.Scripts.Core;
using Game.Scripts.Data;
using Game.Scripts.Units;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private HeroConfig _heroConfig;
        public override void InstallBindings()
        {
            InstallPlayer();
        }

        private void InstallPlayer()
        {
            Container
                .Bind<Rigidbody2D>()
                .FromComponentsInHierarchy()
                .AsSingle();
            
            Container
                .Bind<Transform>()
                .FromInstance(this.gameObject.transform)
                .AsSingle();
            
            Container
                .BindInterfacesTo<MovablePlayer>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<PlayerMotionFsm>()
                .AsSingle();

            Container
                .BindInstance(_heroConfig.abilities)
                .AsSingle();
            
            Container
                .BindInterfacesTo<SpellLogicBase>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<PlayerCombatFsm>()
                .AsSingle();
        }
    }
}

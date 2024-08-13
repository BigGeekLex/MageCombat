using Game.Scripts.Core;
using Game.Scripts.Units;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Game.Scripts.Installers
{
    public class BotInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<NavMeshAgent>()
                .FromComponentsInHierarchy()
                .AsSingle();

            Container
                .Bind<Transform>()
                .FromInstance(this.gameObject.transform)
                .AsSingle();
            
            InstallBotLogic();
        }

        private void InstallBotLogic()
        {
            Container
                .BindInterfacesTo<BotDamageLogic>()
                .FromComponentsInHierarchy()
                .AsSingle();
            
            Container
                .BindInterfacesTo<BotFsm>()
                .AsSingle();
        }
    }
}

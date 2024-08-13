using Game.Scripts.Data;
using Game.Scripts.Units;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class UnitInstaller : MonoInstaller
    {
        [SerializeField] private UnitConfig _unitConfig;
        public override void InstallBindings()
        {
            InstallCommon();
        }

        private void InstallCommon()
        {
            Container
                .BindInstance(_unitConfig)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<UnitModel>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<UnitFacade>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }
    }
}

using System;
using System.Linq;
using Game.Scripts.Data;
using Game.Scripts.Units;
using UniRx;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Scripts.Factories
{
    public interface IUnitSpawner
    {
        IReadOnlyReactiveCollection<UnitFacade> Enemies { get; }
        ReadOnlyReactiveProperty<UnitFacade> Hero { get; }
        void SpawnEnemy(uint enemyId, Vector2 position);
        void SpawnHero(uint heroId, Vector2 position);
    }
    
    public class UnitSpawner : IUnitSpawner, IDisposable
    {
        [Inject] private readonly PrefabsConfig _prefabsConfig;
        
        private ReactiveCollection<UnitFacade> _enemies = new();
        private ReactiveProperty<UnitFacade> _hero = new();
        private CompositeDisposable _lifetimeDisposable = new();
        
        public IReadOnlyReactiveCollection<UnitFacade> Enemies => _enemies;
        public ReadOnlyReactiveProperty<UnitFacade> Hero => _hero.ToReadOnlyReactiveProperty();
        
        public void SpawnEnemy(uint enemyId, Vector2 position)
        {
            UnitData target = _prefabsConfig.Enemies.FirstOrDefault(e => e.UnitConfig.Id == enemyId);
            CompositeDisposable enemyLifetime = new CompositeDisposable();
            
            if (target == null)
                return;
            
            UnitFacade enemy = SpawnUnit(target.UnitPrefab, position);
            
            HandleUnitDead(enemy)
                .Subscribe(_ =>
                {
                    _enemies.Remove(enemy);
                    Object.Destroy(enemy.gameObject);
                    enemyLifetime.Dispose();
                })
                .AddTo(enemyLifetime);
            
            _enemies.Add(enemy);
        }
        
        public void SpawnHero(uint heroId, Vector2 position)
        {
            var target = _prefabsConfig.Heroes.FirstOrDefault(e => e.UnitConfig.Id == heroId);

            if (target == null)
                return;

            UnitFacade hero = SpawnUnit(target.UnitPrefab, position);
            _hero.Value = hero;
        }
        
        public void Dispose()
        {
            _lifetimeDisposable.Dispose();
        }
        
        private UnitFacade SpawnUnit(UnitFacade prefab, Vector2 pos)
        {
            UnitFacade spawnedUnit = Object.Instantiate(prefab, pos, Quaternion.identity);
            return spawnedUnit;
        }

        private IObservable<bool> HandleUnitDead(UnitFacade unit)
        {
            return unit.UnitModel
                .IsDead
                .Where(d => d);
        }
    }
}

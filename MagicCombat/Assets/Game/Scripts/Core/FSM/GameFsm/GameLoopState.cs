using Game.Scripts.Arena;
using Game.Scripts.Data;
using Game.Scripts.Enums;
using Game.Scripts.Factories;
using Game.Scripts.Units;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Core
{
    public class GameLoopState : IState
    {
        private readonly IFsm<EGameState> _fsm;
        private readonly ArenaInfoProvider _arenaInfo;
        private readonly PrefabsConfig _prefabsConfig;
        private readonly IUnitSpawner _unitSpawner;
        private CompositeDisposable _lifetimeDisposable = new();
        
        public GameLoopState(IFsm<EGameState> fsm, ArenaInfoProvider arenaInfo, PrefabsConfig prefabsConfig, IUnitSpawner spawner)
        {
            _fsm = fsm;
            _arenaInfo = arenaInfo;
            _prefabsConfig = prefabsConfig;
            _unitSpawner = spawner;
        }
        
        public void Enter()
        {
            _unitSpawner
                .Enemies
                .ObserveRemove()
                .Subscribe(_ => SpawnEnemy())
                .AddTo(_lifetimeDisposable);
                
            _unitSpawner
                .Hero
                .Where(h => h != null)
                .Subscribe(HandlePlayerDied)
                .AddTo(_lifetimeDisposable);
            
            SpawnPlayer();
            SpawnEnemies();
        }

        public void Exit()
        {
            _lifetimeDisposable.Clear();
        }
        
        public void Dispose()
        {
            _lifetimeDisposable.Dispose();
        }
        private void SpawnPlayer()
        {
            Vector2 playerSpawnPoint = _arenaInfo.ArenaSpawnPoints.PlayerSpawnPoint();
            //Which player we should to spawn? Base on selected hero from 
            //We can have different 
            _unitSpawner.SpawnHero(_prefabsConfig.Heroes[0].UnitConfig.Id, playerSpawnPoint); //Based on selected hero by human
        }
        
        private void SpawnEnemies()
        {
            int maxEnemies = _arenaInfo.ArenaConfig.MaxAliveEnemies;

            for (int i = 0; i < maxEnemies; i++)
            {
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            Vector2[] points = _arenaInfo.ArenaSpawnPoints.EnemySpawnPoints();
            int randomIndex = Random.Range(0, points.Length);
            Vector2 spawnPoint = points[randomIndex];
            randomIndex = Random.Range(0, _prefabsConfig.Enemies.Length);
            uint enemy = _prefabsConfig.Enemies[randomIndex].UnitConfig.Id;
            
            _unitSpawner.SpawnEnemy(enemy, spawnPoint);   
        }

        private void HandlePlayerDied(UnitFacade hero)
        {
            hero.UnitModel.IsDead
                .Where(d => d)
                .Subscribe(_ => OnPlayerDied())
                .AddTo(_lifetimeDisposable);
        }
        
        private void OnPlayerDied()
        {
            _fsm.Enter(EGameState.Bootstrap);
        }
    }
}

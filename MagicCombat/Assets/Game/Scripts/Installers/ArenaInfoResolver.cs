using Game.Scripts.Arena;
using Game.Scripts.Data;
using Zenject;

namespace Game.Scripts.Installers
{
    public class ArenaInfoResolver : IInitializable
    {
        private readonly ArenaInfoProvider _arenaInfoProvider;
        private readonly ArenaSpawnPoints _arenaSpawnPoints;
        private readonly ArenaConfig _arenaConfig;

        public ArenaInfoResolver(ArenaInfoProvider arenaInfoProvider, 
            [Inject(Source = InjectSources.Local, Optional = true)] 
            ArenaSpawnPoints arenaSpawnPoints,
            [Inject(Source = InjectSources.Local, Optional = true)]
            ArenaConfig arenaConfig)
        {
            _arenaInfoProvider = arenaInfoProvider;
            _arenaSpawnPoints = arenaSpawnPoints;
            _arenaConfig = arenaConfig;
        }

        public void Initialize()
        {
            _arenaInfoProvider.InitializeArena(_arenaSpawnPoints, _arenaConfig); 
        }
    }

}

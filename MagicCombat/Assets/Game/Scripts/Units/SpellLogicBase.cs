using Game.Scripts.Core;
using Game.Scripts.Data;
using Game.Scripts.Factories;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Units
{
    
    public interface ISpellLogic
    {
        int TotalSpells { get; }
        void CastSpell(int index, Vector2 dir);
    }
    
    public class SpellLogicBase : ISpellLogic
    {
        [Inject] private readonly IProjectileSpawner _projectileSpawner;
        [Inject] private readonly UnitFacade _unitFacade;
        [Inject] private readonly SpellConfig[] _spells;

        public void CastSpell(int index, Vector2 dir)
        {
            if (index >= _spells.Length || index < 0)
                return;

            var targetAbilityConfig = _spells[index];
            
            SpawnProjectile(targetAbilityConfig, dir);
        }

        public int TotalSpells => _spells.Length;

        private void SpawnProjectile(SpellConfig target, Vector2 dir)
        {
            ProjectileInput pi = new ProjectileInput(dir, _unitFacade.ProjectileSpawnPosition, _unitFacade, target);
            _projectileSpawner.Spawn(pi);
        }
    }
}

using Game.Scripts.Enums;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Units
{
    public class UnitFacade : MonoBehaviour
    {
        [SerializeField] private EUnitCategory _unitCategory;
        [SerializeField] private Transform _projectileSpawn;

        public EUnitCategory UnitCategory => _unitCategory;
        public Vector2 ProjectileSpawnPosition => _projectileSpawn.position;   
        
        [Inject] public readonly UnitModel UnitModel;
    }
}

using Game.Scripts.Enums;
using Game.Scripts.Units;
namespace Game.Scripts.Core
{
    public struct DamageInput
    {
        public UnitFacade Source;
        public UnitFacade Target;
        public bool IsDamageOverride;
        public ECalcType CalcSourceDamage;
        public float SourceOverrideDamage;
    }
}

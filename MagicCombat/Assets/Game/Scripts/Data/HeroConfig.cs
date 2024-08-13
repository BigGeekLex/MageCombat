using UnityEngine;

namespace Game.Scripts.Data
{
    [CreateAssetMenu(menuName = "Configs/Hero", fileName = "Hero", order = 0)]
    public class HeroConfig : UnitConfig
    {
        public SpellConfig[] abilities;
    }
}

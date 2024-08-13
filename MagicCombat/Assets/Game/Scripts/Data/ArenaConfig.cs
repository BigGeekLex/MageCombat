using UnityEngine;
namespace Game.Scripts.Data
{
    [CreateAssetMenu(menuName = "Configs/Arena", fileName = "Arena", order = 0)]
    public class ArenaConfig : ScriptableObject
    {
        public int MaxAliveEnemies;
        public Vector2 Size;
    }
}

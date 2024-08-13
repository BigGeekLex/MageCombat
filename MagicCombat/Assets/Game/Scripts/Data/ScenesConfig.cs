using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Data
{
    [CreateAssetMenu(menuName = "Configs/Scenes", fileName = "Scenes", order = 0)]
    public class ScenesConfig : ScriptableObject
    {
        public SceneAsset Bootstrap;
        public SceneAsset[] Arenas;
    }
}

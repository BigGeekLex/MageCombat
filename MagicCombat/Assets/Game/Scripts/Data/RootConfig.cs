using UnityEngine;
namespace Game.Scripts.Data
{
    [CreateAssetMenu(menuName = "Configs/Root", fileName = "Root")]
    public class RootConfig : ScriptableObject
    {
        public ScenesConfig scenesConfig;
        public PrefabsConfig prefabsConfig;
    }

}

using System;
using UnityEditor;
using UnityEngine;
namespace Game.Scripts.Data
{
    public abstract class AutoIdConfig : ScriptableObject
    {
        public uint Id => _id;
        
        [SerializeField] private uint _id;
        
        protected virtual void OnValidate()
        {
            AssignId(); 
        }

        private void AssignId()
        {
#if UNITY_EDITOR

            string path = AssetDatabase.GetAssetPath(this);

            if (string.IsNullOrWhiteSpace(path))
                return;
            
            Guid guid = new Guid(AssetDatabase.AssetPathToGUID(path));
            _id	 = (uint) guid.GetHashCode();
#endif
        }
    }
}

using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Core
{
    public interface ISceneLoader
    {
        Subject<Scene> OnSceneLoaded {get;}
       void Load(string scene, bool isActivateScene = true); 
    }
    
    public class SceneLoader : MonoBehaviour, ISceneLoader
    {
        private Subject<Scene> _onSceneLoaded = new();

        public Subject<Scene> OnSceneLoaded => _onSceneLoaded;
        public void Load(string scene, bool isActivateScene = true) => StartCoroutine(LoadScene(scene, isActivateScene));
        
        private IEnumerator LoadScene(string scene, bool isActivateScene)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(scene);

            while (!ao.isDone) 
                yield return null;

            var loaded = SceneManager.GetSceneByName(scene);
            
            if (isActivateScene) 
                SceneManager.SetActiveScene(loaded);

            OnSceneLoaded.OnNext(loaded);
        }
    }
}

using System;
using UniRx;
using UnityEngine.InputSystem;

namespace Game.Scripts.Extensions
{
    public static class ExtensionUniRx
    {
        //https://github.com/neuecc/UniRx/issues/481
        public static IObservable<InputAction.CallbackContext> PerformedAsObservable(this InputAction action)
        {
            return Observable.FromEvent<InputAction.CallbackContext>(
                h => action.performed += h, 
                h => action.performed -= h);   
        }

        public static IObservable<InputAction.CallbackContext> StartedAsObservable(this InputAction action)
        {
            return Observable.FromEvent<InputAction.CallbackContext>(
                h => action.started += h,
                h => action.started -= h);   
        }
    }
}

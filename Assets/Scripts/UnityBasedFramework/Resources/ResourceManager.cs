#region FILE HEADER
// Filename: ResourceManager.cs
// Author: Kalulas
// Create: 2022-11-06
// Description:
// Design ->
// GPP ->
#endregion

using System;
using System.Threading.Tasks;
using Framework.DesignPattern;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UnityBasedFramework.Resources
{
    public class ResourceManager : Singleton<ResourceManager>
    {
        #region Singleton

        private ResourceManager()
        {
            Debug.Log("[ResourceManager.ResourceManager] ctor");
        }

        public override void OnSingletonInit()
        {
            // throw new System.NotImplementedException();
        }

        public override void OnSingletonDisposed()
        {
            // throw new System.NotImplementedException();
        }

        #endregion

        #region Public Interface

        public async Task<T> LoadResourceAsync<T>(string key)
        {
            // TODO resource management...
            var result = await Addressables.LoadAssetAsync<T>(key).Task;
            return result;
        }
        
        public async Task<GameObject> LoadAndInstantiateAsync<T>(string key)
        {
            var result = await Addressables.InstantiateAsync(key).Task;
            return result;
        }


        public /*async*/ void LoadResourceAsync<T>(string key, Action<bool, T> onLoadCompleted)
        {
            // TODO resource management...
            Addressables.LoadAssetAsync<T>(key).Completed += handle =>
            {
                onLoadCompleted?.Invoke(handle.Status == AsyncOperationStatus.Succeeded, handle.Result);
            };

            // TODO if loaded, do it like:
            // await Task.Yield();
            // onLoadCompleted.Invoke(false, default(T));
        }
        
        #endregion

    }
}
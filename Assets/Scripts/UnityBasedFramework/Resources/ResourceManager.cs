#region FILE HEADER
// Filename: ResourceManager.cs
// Author: Kalulas
// Create: 2022-11-06
// Description:
// Design ->
// GPP ->
#endregion

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.DesignPattern;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace UnityBasedFramework.Resources
{
    public class ResourceManager : Singleton<ResourceManager>
    {
        #region Fields

        private Dictionary<string, AsyncOperationHandle> m_LoadOperationHandleDict;

        #endregion
        
        #region Singleton

        private ResourceManager()
        {
            Debug.Log("[ResourceManager.ResourceManager] ctor");
        }

        public override void OnSingletonInit()
        {
            m_LoadOperationHandleDict = new Dictionary<string, AsyncOperationHandle>();
        }

        public override void OnSingletonDisposed()
        {
            m_LoadOperationHandleDict.Clear();
            m_LoadOperationHandleDict = null;
        }

        #endregion

        #region Public Interface

        /// <summary>
        /// Load with addressable and return instantiated object
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="TObject">The type of the asset.</typeparam>
        /// <returns></returns>
        public async Task<TObject> LoadAndInstantiateAsync<TObject>(string key) where TObject : Object
        {
            Debug.Log($"[ResourceManager.LoadAndInstantiateAsync] load asset '{key}'");
            if (m_LoadOperationHandleDict.TryGetValue(key, out var loadHandle) && 
                loadHandle.Result is TObject targetAsset)
            {
                Debug.Log($"[ResourceManager.LoadAndInstantiateAsync] asset '{key}' in LoadHandleDict, yield and return");
                await Task.Yield();
                return ObjectProxy.Instantiate(targetAsset);
            }
            
            var handle = Addressables.LoadAssetAsync<TObject>(key);
            var result = await handle.Task;
            if (handle.Status == AsyncOperationStatus.Succeeded && handle.Result != null)
            {
                if (!m_LoadOperationHandleDict.ContainsKey(key))
                {
                    m_LoadOperationHandleDict.Add(key, handle);
                    Debug.Log($"[ResourceManager.LoadAndInstantiateAsync] asset '{key}' added into LoadHandleDict");
                }

                return ObjectProxy.Instantiate(result);
            }
            
            Debug.LogError($"[ResourceManager.LoadAndInstantiateAsync] fails to load asset '{key}', return null");
            return null;
        }

        /// <summary>
        /// Load with addressable and return loaded asset
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="TObject">The type of the asset.</typeparam>
        /// <returns></returns>
        public async Task<TObject> LoadResourceAsync<TObject>(string key) where TObject : Object
        {
            Debug.Log($"[ResourceManager.LoadResourceAsync] load asset '{key}'");
            if (m_LoadOperationHandleDict.TryGetValue(key, out var loadHandle) &&
                loadHandle.Result is TObject targetAsset)
            {
                Debug.Log($"[ResourceManager.LoadResourceAsync] asset '{key}' in cache, yield and return");
                await Task.Yield();
                return targetAsset;
            }

            var handle = Addressables.LoadAssetAsync<TObject>(key);
            var result = await handle.Task;
            if (handle.Status == AsyncOperationStatus.Succeeded && !m_LoadOperationHandleDict.ContainsKey(key))
            {
                m_LoadOperationHandleDict.Add(key, handle);
                Debug.Log($"[ResourceManager.LoadResourceAsync] asset '{key}' added into cache");
            }

            if (handle.Status == AsyncOperationStatus.Succeeded && handle.Result != null)
            {
                if (!m_LoadOperationHandleDict.ContainsKey(key))
                {
                    m_LoadOperationHandleDict.Add(key, handle);
                    Debug.Log($"[ResourceManager.LoadResourceAsync] asset '{key}' added into cache");
                }

                return result;
            }

            Debug.LogError($"[ResourceManager.LoadResourceAsync] fails to load asset '{key}', return null");
            return null;
        }

        public bool UnloadResource(string key)
        {
            Debug.Log($"[ResourceManager.UnloadResource] release asset '{key}'");
            if (m_LoadOperationHandleDict.TryGetValue(key, out var loadHandle))
            {
                Addressables.Release(loadHandle);
                m_LoadOperationHandleDict.Remove(key);
                return true;
            }

            Debug.LogError($"[ResourceManager.UnloadResource] asset '{key}' not found in LoadHandleDict");
            return false;
        }

        /// <summary>
        /// Release all resources with loaded operation handle
        /// </summary>
        /// <param name="unloadUnused"> if true, UnityEngine.Resources.UnloadUnusedAssets() is called </param>
        public void UnloadAllLoaded(bool unloadUnused)
        {
            foreach (var loadHandle in m_LoadOperationHandleDict.Values)
            {
                Addressables.Release(loadHandle);
            }
            
            m_LoadOperationHandleDict.Clear();
            if (unloadUnused)
            {
                UnityEngine.Resources.UnloadUnusedAssets();
            }
        }

        #endregion

    }
}
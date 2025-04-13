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
using Framework.Debug;
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
            Log.Info("[ResourceManager.LoadAndInstantiateAsync] load asset '{0}'", key);
            if (m_LoadOperationHandleDict.TryGetValue(key, out var loadHandle) && 
                loadHandle.Result is TObject targetAsset)
            {
                Log.Info("[ResourceManager.LoadAndInstantiateAsync] asset '{0}' in LoadHandleDict, yield and return", key);
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
                    Log.Info("[ResourceManager.LoadAndInstantiateAsync] asset '{0}' added into LoadHandleDict", key);
                }

                return ObjectProxy.Instantiate(result);
            }
            
            Log.Error("[ResourceManager.LoadAndInstantiateAsync] fails to load asset '{0}', return null", key);
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
            Log.Info("[ResourceManager.LoadResourceAsync] load asset '{0}'", key);
            if (m_LoadOperationHandleDict.TryGetValue(key, out var loadHandle) &&
                loadHandle.Result is TObject targetAsset)
            {
                Log.Info("[ResourceManager.LoadResourceAsync] asset '{0}' in cache, yield and return", key);
                await Task.Yield();
                return targetAsset;
            }

            var handle = Addressables.LoadAssetAsync<TObject>(key);
            var result = await handle.Task;
            if (handle.Status == AsyncOperationStatus.Succeeded && !m_LoadOperationHandleDict.ContainsKey(key))
            {
                m_LoadOperationHandleDict.Add(key, handle);
                Log.Info("[ResourceManager.LoadResourceAsync] asset '{0}' added into cache", key);
            }

            if (handle.Status == AsyncOperationStatus.Succeeded && handle.Result != null)
            {
                if (!m_LoadOperationHandleDict.ContainsKey(key))
                {
                    m_LoadOperationHandleDict.Add(key, handle);
                    Log.Info("[ResourceManager.LoadResourceAsync] asset '{0}' added into cache", key);
                }

                return result;
            }

            Log.Error("[ResourceManager.LoadResourceAsync] fails to load asset '{0}', return null", key);
            return null;
        }

        public bool UnloadResource(string key)
        {
            Log.Info("[ResourceManager.UnloadResource] release asset '{0}'", key);
            if (m_LoadOperationHandleDict.TryGetValue(key, out var loadHandle))
            {
                Addressables.Release(loadHandle);
                m_LoadOperationHandleDict.Remove(key);
                return true;
            }

            Log.Error("[ResourceManager.UnloadResource] asset '{0}' not found in LoadHandleDict", key);
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
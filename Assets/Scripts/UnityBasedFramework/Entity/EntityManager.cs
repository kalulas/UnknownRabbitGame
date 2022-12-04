#region FILE HEADER
// Filename: EntityManager.cs
// Author: Kalulas
// Create: 2022-11-06
// Description:
// Design ->
// GPP ->
#endregion

using System;
using System.Collections.Generic;
using Framework.DesignPattern;
using UnityBasedFramework.Resources;
using UnityEngine;

namespace UnityBasedFramework.Entity
{
    public class EntityManager : Singleton<EntityManager>
    {
        #region Fields

        /// <summary>
        /// To Class Generator
        /// </summary>
        private uint m_EntityIDGenerator;
        private List<Entity> m_EntityList;
        private Dictionary<uint, Entity> m_EntityDict;

        #endregion
        
        #region Singleton

        private EntityManager()
        {
            Debug.Log("[EntityManager.EntityManager] ctor");
        }

        public override void OnSingletonInit()
        {
            m_EntityIDGenerator = 0;
            m_EntityList = new List<Entity>();
            m_EntityDict = new Dictionary<uint, Entity>();
        }

        public override void OnSingletonDisposed()
        {
            m_EntityList.Clear();
            m_EntityDict.Clear();
            m_EntityList = null;
            m_EntityDict = null;
        }

        #endregion

        #region Event Function

        public void Update(float deltaTime)
        {
            for (int i = 0; i < m_EntityList.Count; i++)
            {
                m_EntityList[i].Update(deltaTime);
            }
        }

        public void FrameUpdate(float frameLength)
        {
            for (int i = 0; i < m_EntityList.Count; i++)
            {
                m_EntityList[i].FrameUpdate(frameLength);
            }
        }

        #endregion

        #region Private

        private void AddEntityToContainer(uint entityID, Entity entity)
        {
            m_EntityList.Add(entity);
            m_EntityDict.Add(entityID, entity);
        }

        private uint CreateEntityInternal(GameObject gameObject)
        {
            var entityID = m_EntityIDGenerator++;
            var entity = Entity.CreateEntity(entityID);
            entity.BindGameObject(gameObject);
            AddEntityToContainer(entityID, entity);
            return entityID;
        }

        #endregion

        #region Public Interface

        public async void CreateEntity(string resourceKey, Transform parent, Action<bool, uint, GameObject> afterEntityCreated)
        {
            var entityInstance = await ResourceManager.Instance.LoadAndInstantiateAsync<GameObject>(resourceKey);
            entityInstance.transform.SetParent(parent, false);
            var entityID = CreateEntityInternal(entityInstance);
            afterEntityCreated?.Invoke(true, entityID, entityInstance);
        }

        public Entity GetEntity(uint entityID)
        {
            if (!m_EntityDict.TryGetValue(entityID, out var entity))
            {
                Debug.LogError($"[EntityManager.GetEntity] entityID'{entityID}' illegal, exit!");
                return null;
            }

            return entity;
        }

        #endregion

    }
}
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
using Framework.Logging;
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
        private List<BaseEntity> m_EntityList;
        private Dictionary<uint, BaseEntity> m_EntityDict;

        #endregion
        
        #region Singleton

        private EntityManager()
        {
            
        }

        public override void OnSingletonInit()
        {
            m_EntityIDGenerator = 0;
            m_EntityList = new List<BaseEntity>();
            m_EntityDict = new Dictionary<uint, BaseEntity>();
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

        private void AddEntityToContainer(uint entityID, BaseEntity baseEntity)
        {
            m_EntityList.Add(baseEntity);
            m_EntityDict.Add(entityID, baseEntity);
        }

        private uint CreateEntityInternal<TEntity>(GameObject gameObject) where TEntity : BaseEntity, new()
        {
            var entityID = m_EntityIDGenerator++;
            var entity = BaseEntity.CreateEntity<TEntity>(entityID).BindGameObject(gameObject).AttachComponents();
            AddEntityToContainer(entityID, entity);
            return entityID;
        }

        #endregion

        #region Public Interface

        public async void CreateEntity<TEntity>(string resourceKey, Transform parent, Action<bool, uint, GameObject> afterEntityCreated) where TEntity : BaseEntity, new()
        {
            var entityInstance = await ResourceManager.Instance.LoadAndInstantiateAsync<GameObject>(resourceKey);
            entityInstance.transform.SetParent(parent, false);
            var entityID = CreateEntityInternal<TEntity>(entityInstance);
            afterEntityCreated?.Invoke(true, entityID, entityInstance);
        }

        public BaseEntity GetEntity(uint entityID)
        {
            if (!m_EntityDict.TryGetValue(entityID, out var entity))
            {
                Log.Error("[EntityManager.GetEntity] entityID '{0}' illegal, exit!", entityID);
                return null;
            }

            return entity;
        }

        #endregion

    }
}
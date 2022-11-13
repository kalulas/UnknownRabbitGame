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
using Object = UnityEngine.Object;

namespace UnityBasedFramework.Entity
{
    public class EntityManager : Singleton<EntityManager>
    {
        #region Fields

        private List<Entity> m_EntityList;

        #endregion
        
        #region Singleton

        private EntityManager()
        {
            Debug.Log("[EntityManager.EntityManager] ctor");
        }

        public override void OnSingletonInit()
        {
            m_EntityList = new List<Entity>();
        }

        public override void OnSingletonDisposed()
        {
            m_EntityList.Clear();
            m_EntityList = null;
        }

        #endregion

        #region Event Function

        public void Update()
        {
            for (int i = 0; i < m_EntityList.Count; i++)
            {
                m_EntityList[i].Update();
            }
        }

        #endregion

        #region Private

        private int AddEntityToContainer(Entity entity)
        {
            var entityID = m_EntityList.Count;
            m_EntityList.Add(entity);
            return entityID;
        }

        private int CreateEntityInternal(GameObject gameObject)
        {
            var entity = new Entity();
            entity.BindGameObject(gameObject);
            var entityID = AddEntityToContainer(entity);
            return entityID;
        }

        #endregion

        #region Public Interface

        public void CreateEntity(string resourceKey, Transform parent, Action<bool, int, GameObject> afterEntityCreated)
        {
            ResourceManager.Instance.LoadResourceAsync<GameObject>(resourceKey, (success, entityRes) =>
            {
                if (!success)
                {
                    Debug.LogError("[EntityManager.CreateEntity] load entity resource failed, exit!");
                    return;
                }
                
                var entityInstance = Object.Instantiate(entityRes, parent);
                var entityID = CreateEntityInternal(entityInstance);
                afterEntityCreated?.Invoke(true, entityID, entityInstance);
            });
        }

        public Entity GetEntity(int entityID)
        {
            if (entityID < 0 || entityID >= m_EntityList.Count)
            {
                Debug.LogError($"[EntityManager.GetEntity] entityID'{entityID}' illegal, exit!");
                return null;
            }

            return m_EntityList[entityID];
        }

        #endregion

    }
}
#region FILE HEADER
// Filename: Entity.cs
// Author: Kalulas
// Create: 2022-11-06
// Description:
// Design ->
// GPP ->
#endregion

using Framework.Logging;
using UnityEngine;

namespace UnityBasedFramework.Entity
{
    public class BaseEntity
    {
        protected uint m_EntityID;
        protected GameObject m_GameObject;

        #region Properties

        public uint EntityID => m_EntityID;
        public GameObject GameObject => m_GameObject;

        #endregion

        #region Event Function

        public virtual void Update(float deltaTime)
        {
            
        }

        public virtual void FrameUpdate(float frameLength)
        {
            
        }

        protected virtual void OnAttachComponents()
        {
            
        }

        public BaseEntity AttachComponents()
        {
            OnAttachComponents();
            return this;
        }
        
        public BaseEntity BindGameObject(GameObject targetGameObject)
        {
            if (m_GameObject != null)
            {
                Log.Warning("[BaseEntity.BindGameObject] original GameObject:{0}, rebind is illegal", m_GameObject.name);
                return this;
            }

            m_GameObject = targetGameObject;
            return this;
        }

        #endregion

        #region Private Util

        private BaseEntity LateConstruct(uint entityID)
        {
            m_EntityID = entityID;
            return this;
        }

        #endregion

        #region Public Interface

        public static BaseEntity CreateEntity<TEntity>(uint entityID) where TEntity : BaseEntity, new()
        {
            return new TEntity().LateConstruct(entityID);
        }

        #endregion
    }
}
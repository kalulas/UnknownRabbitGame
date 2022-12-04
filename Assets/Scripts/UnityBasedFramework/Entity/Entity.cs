#region FILE HEADER
// Filename: Entity.cs
// Author: Kalulas
// Create: 2022-11-06
// Description:
// Design ->
// GPP ->
#endregion

using UnityEngine;

namespace UnityBasedFramework.Entity
{
    public class Entity
    {
        private uint m_EntityID;
        protected GameObject m_GameObject;

        #region Properties

        public uint EntityID => m_EntityID;
        public GameObject GameObject => m_GameObject;

        #endregion

        #region Event Function

        private Entity(uint entityID)
        {
            m_EntityID = entityID;
        }

        public virtual void Update(float deltaTime)
        {
            
        }

        public virtual void FrameUpdate(float frameLength)
        {
            
        }

        #endregion

        #region Public Interface

        public static Entity CreateEntity(uint entityID)
        {
            return new Entity(entityID);
        }

        public void BindGameObject(GameObject targetGameObject)
        {
            if (m_GameObject != null)
            {
                Debug.LogWarning($"[Entity.BindGameObject] original GameObject:{m_GameObject.name}, rebind is illegal");
                return;
            }

            m_GameObject = targetGameObject;
        }

        #endregion
    }
}
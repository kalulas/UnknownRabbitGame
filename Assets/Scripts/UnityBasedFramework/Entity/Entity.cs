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
        protected GameObject m_GameObject;

        #region Properties

        public GameObject GameObject => m_GameObject;

        #endregion

        #region Event Function

        public virtual void Update()
        {
            
        }

        #endregion

        #region Public Interface

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
#region FILE HEADER
// Filename: PlayerEntity.cs
// Author: Kalulas
// Create: 2022-12-04
// Description:
#endregion

using Framework.InputSystem;
using UnityBasedFramework.Entity;
using UnknownRabbitGame.InputSystem;
using UnknownRabbitGame.MonoComponent.Dispatcher;

namespace UnknownRabbitGame.Entity
{
    public class PlayerEntity : BaseEntity
    {
        #region Event Function

        protected override void OnAttachComponents()
        {
            base.OnAttachComponents();
            m_GameObject.AddComponent<ScreenCornerTest>();
            // TODO to entity based component
            m_GameObject.AddComponent<TestInputReceiver>();
        }

        #endregion
    }
}
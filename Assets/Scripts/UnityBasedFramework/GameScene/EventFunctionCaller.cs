#region FILE HEADER
// Filename: EventFunctionBridge.cs
// Author: Kalulas
// Create: 2022-06-19
// Description:
// Design ->
// GPP ->
#endregion

using Framework.GameScene;
using UnityEngine;

namespace UnityBasedFramework.GameScene
{
    public abstract class EventFunctionCaller : MonoBehaviour, IEventFunctionCaller
    {
        public abstract void OnCallerAwake();
        public abstract void OnCallerStart();
        public abstract void OnCallerUpdate();
        public abstract void OnCallerDestroy();
        
        
        private void Awake()
        {
            OnCallerAwake();
        }

        private void Start()
        {
            OnCallerStart();
        }

        private void Update()
        {
            OnCallerUpdate();
        }

        private void OnDestroy()
        {
            OnCallerDestroy();
        }
    }
}
#region FILE HEADER
// Filename: IEventFunctionCaller.cs
// Author: Kalulas
// Create: 2022-10-31
// Description:
// Design ->
// GPP ->
#endregion

namespace Framework.GameScene
{
    public interface IEventFunctionCaller
    {
        void OnCallerAwake();
        void OnCallerStart();
        void OnCallerUpdate(float deltaTime);
        void OnCallerFixedUpdate(float fixedDeltaTime);
        void OnCallerDestroy();
    }
}
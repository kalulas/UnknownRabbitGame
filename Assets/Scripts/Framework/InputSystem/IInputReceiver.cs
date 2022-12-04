#region FILE HEADER
// Filename: IInputReceiver.cs
// Author: Kalulas
// Create: 2022-12-04
// Description:
#endregion

namespace Framework.InputSystem
{
    public interface IInputReceiver
    {
        void Receive(InputMessage[] messages);
    }
}
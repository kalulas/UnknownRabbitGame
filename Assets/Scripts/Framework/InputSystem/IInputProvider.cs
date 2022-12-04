#region FILE HEADER
// Filename: IInputProvider.cs
// Author: Kalulas
// Create: 2022-12-04
// Description:
#endregion

namespace Framework.InputSystem
{
    public interface IInputProvider
    {
        // is this necessary?
        // void SetInputIdentifier(uint identifier);
        InputMessage[] Provide();
    }
}
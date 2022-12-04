#region FILE HEADER
// Filename: InputMessage.cs
// Author: Kalulas
// Create: 2022-12-04
// Description:
#endregion

namespace Framework.InputSystem
{
    public struct InputMessage
    {
        public ushort InputType;
        public float InputValue0;
        public float InputValue1;
        public float InputValue2;
        public float InputValue3;
    }
}
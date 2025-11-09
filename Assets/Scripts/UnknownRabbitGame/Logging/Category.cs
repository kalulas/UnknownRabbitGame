#region FILE HEADER

// Filename: Category.cs
// Author: Kalulas
// Create: 2025-11-09
// Description:

#endregion

using System;
using Framework.Logging;

namespace UnknownRabbitGame.Logging
{
    [Flags]
    public enum Category : ulong
    {
        None = Log.NoCategory,
        Entry = 1 << 0,
        GameMode = 1 << 1,
        Resources = 1 << 2,
    }
}
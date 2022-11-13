#region FILE HEADER
// Filename: ObjectProxy.cs
// Author: Kalulas
// Create: 2022-11-13
// Description:
// Design ->
// GPP ->
#endregion

using UnityEngine;

namespace UnityBasedFramework.Resources
{
    public static class ObjectProxy
    {
        public static T Instantiate<T>(T original) where T : Object
        {
            return Object.Instantiate(original);
        }

        public static void Destroy(Object original)
        {
            Object.Destroy(original);
        }
    }
}
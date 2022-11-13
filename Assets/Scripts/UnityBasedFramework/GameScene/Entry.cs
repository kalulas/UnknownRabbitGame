#region FILE HEADER
// Filename: Entry.cs
// Author: Kalulas
// Create: 2022-10-31
// Description:
// Design ->
// GPP ->
#endregion

using Framework.GameScene;
using UnityEngine;

namespace UnityBasedFramework.GameScene
{
    public abstract class Entry : MonoBehaviour, IEntry
    {
        public abstract void OnEntryAwake();

        private void Awake()
        {
            DontDestroyOnLoad(this);
            OnEntryAwake();
        }
    }
}
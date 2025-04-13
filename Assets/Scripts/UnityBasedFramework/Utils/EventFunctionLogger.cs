using Framework.Debug;
using UnityEngine;

namespace UnityBasedFramework.Utils
{
    public class EventFunctionLogger : MonoBehaviour
    {
        private void Awake()
        {
            Log.Info("[EventFunctionLogger.Awake] {0}", name);
        }

        private void OnEnable()
        {
            Log.Info("[EventFunctionLogger.OnEnable] {0}", name);
        }

        private void OnDisable()
        {
            Log.Info("[EventFunctionLogger.OnDisable] {0}", name);
        }
    
        private void OnDestroy()
        {
            Log.Info("[EventFunctionLogger.OnDestroy] {0}", name);
        }

        private void Start()
        {
            Log.Info("[EventFunctionLogger.Start] {0}", name);
        }

        private void OnTriggerEnter(Collider other)
        {
            Log.Info("[EventFunctionLogger.OnTriggerEnter] '{0}' invoked by '{1}'", name, other.name);
        }
    
        private void OnTriggerExit(Collider other)
        {
            Log.Info("[EventFunctionLogger.OnTriggerExit] '{0}' invoked by '{1}'", name, other.name);
        }
    }
}

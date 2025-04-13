using UnityEngine;

namespace UnityBasedFramework.Utils
{
    public class EventFunctionLogger : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log($"[EventFunctionLogger.Awake] {name}");
        }

        private void OnEnable()
        {
            Debug.Log($"[EventFunctionLogger.OnEnable] {name}");
        }

        private void OnDisable()
        {
            Debug.Log($"[EventFunctionLogger.OnDisable] {name}");
        }
    
        private void OnDestroy()
        {
            Debug.Log($"[EventFunctionLogger.OnDestroy] {name}");
        }

        private void Start()
        {
            Debug.Log($"[EventFunctionLogger.Start] {name}");
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"[EventFunctionLogger.OnTriggerEnter] {name} invoked by {other.name}");
        }
    
        private void OnTriggerExit(Collider other)
        {
            Debug.Log($"[EventFunctionLogger.OnTriggerExit] {name} invoked by {other.name}");
        }
    }
}

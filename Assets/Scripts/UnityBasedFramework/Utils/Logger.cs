using UnityEngine;

namespace UnityBasedFramework.Utils
{
    public class Logger : MonoBehaviour
    {
        private void Awake()
        {
            Debug.LogFormat("Awake: {0} {1}", name, Time.frameCount);
        }

        private void OnEnable()
        {
            Debug.LogFormat("OnEnable: {0} {1}", name, Time.frameCount);
        }

        // Start is called before the first frame update
        void Start()
        {
            Debug.LogFormat("Start: {0} {1}", name, Time.frameCount);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}

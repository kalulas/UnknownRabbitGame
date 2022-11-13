using UnityEngine;

namespace UnityBasedFramework.Utils
{
    /// <summary>
    /// tmp solution for ResourceManager
    /// </summary>
    public class Loader : MonoBehaviour
    {
        public string LoadResourceName;
    
        // Start is called before the first frame update
        void Start()
        {
            var go = UnityEngine.Resources.Load<GameObject>(LoadResourceName);
            var instance = Instantiate(go, transform);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}

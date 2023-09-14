using UnityEngine;

namespace Utilities
{
    public class MonoSingletonBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }

}
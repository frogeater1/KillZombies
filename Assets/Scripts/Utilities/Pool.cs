using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Utilities
{
    public class Pool<T> : ObjectPool<T> where T : MonoBehaviour
    {
        public Transform container;
        public Transform defaultParent;

        private readonly LinkedList<T> _live = new();

        public Pool(T prefab, Func<T> createFunc = null, Action<T> actionOnGet = null, Action<T> actionOnRelease = null, Action<T> actionOnDestroy = null,
            bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000, Transform defaultParent = null, Transform container = null) : base(
            createFunc == null ? () => MonoBehaviour.Instantiate(prefab, container) : null,
            actionOnGet == null ? result => result.gameObject.SetActive(true) : null,
            actionOnRelease == null ? (result) => result.gameObject.SetActive(false) : null,
            actionOnDestroy == null ? MonoBehaviour.Destroy : null,
            collectionCheck, defaultCapacity, maxSize)
        {
            this.container = container;
            this.defaultParent = defaultParent;
        }

        public TY Get<TY>(Vector3 position = default, Quaternion rotation = default, Transform parent = null) where TY : T
        {
            var result = base.Get();
            result.transform.SetParent(parent ? parent : defaultParent, false);
            result.transform.SetPositionAndRotation(position, rotation);
            result.enabled = true;
            _live.AddLast(result);
            return (TY)result;
        }

        public new void Release(T obj)
        {
            base.Release(obj);
            if (container) obj.transform.SetParent(container);
            _live.Remove(obj);
        }

        public new void Clear()
        {
            base.Clear();
            _live.Clear();
        }

        public IEnumerable<T> Instances()
        {
            return _live;
        }
    }
}
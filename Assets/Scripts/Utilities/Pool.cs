using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Utilities
{
    public class Pool<T> where T : MonoBehaviour
    {
        public Transform container;
        private readonly Dictionary<T, T> _live = new();
        private readonly Dictionary<T, ObjectPool<T>> _pool = new();

        public TY Get<TY>(TY prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where TY : T
        {
            if (!_pool.TryGetValue(prefab, out var pool))
            {
                _pool.Add(prefab,
                    pool = new ObjectPool<T>(createFunc: () => Object.Instantiate(prefab),
                        actionOnRelease: obj =>
                        {
                            obj.gameObject.SetActive(false);
                            obj.transform.SetParent(container);
                            _live.Remove(obj);
                        },
                        actionOnDestroy: Object.Destroy));
            }

            var result = (TY)pool.Get();
            result.transform.SetParent(parent, false);
            result.transform.SetPositionAndRotation(position, rotation);
            result.gameObject.SetActive(true);
            result.enabled = true;
            _live[result] = prefab;

            return result;
        }

        public void Release(T obj)
        {
            _pool[_live[obj]].Release(obj);
        }

        public IEnumerable<T> Instances()
        {
            return _live.Keys;
        }

        public void ClearAll()
        {
            _live.Clear();

            foreach (var pool in _pool.Values)
            {
                pool.Clear();
            }
        }
    }
}
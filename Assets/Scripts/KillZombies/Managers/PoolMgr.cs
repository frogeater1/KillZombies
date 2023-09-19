using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

namespace KillZombies.Managers
{
    public class PoolMgr : MonoBehaviour
    {
        public readonly Dictionary<MonoBehaviour, Pool<MonoBehaviour>> pools = new();

        public void SetPool(MonoBehaviour prefab, Func<MonoBehaviour> createFunc = null,
            Action<MonoBehaviour> actionOnGet = null, Action<MonoBehaviour> actionOnRelease = null, Action<MonoBehaviour> actionOnDestroy = null,
            bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000, Transform defaultParent = null, Transform container = null)
        {
            var pool = new Pool<MonoBehaviour>(prefab, createFunc, actionOnGet, actionOnRelease, actionOnDestroy, collectionCheck, defaultCapacity, maxSize,
                defaultParent, container);
            pools.Add(prefab, pool);
        }


        public T GetFromPool<T>(T prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where T : MonoBehaviour
        {
            return pools.TryGetValue(prefab, out var pool) ? pool.Get<T>(position, rotation, parent) : null;
        }


        public void LogicalUpdate()
        {
            foreach (var bullet in pools.Keys.OfType<Bullet>().SelectMany(prefab => pools[prefab].Instances()).Cast<Bullet>())
            {
                bullet.LogicalUpdate();
            }
        }
    }
}
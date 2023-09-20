using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

namespace KillZombies.Managers
{
    public class PoolMgr : MonoBehaviour
    {
        public Transform bulletContainer;
        public Pool<Bullet> bulletPool;

        public void Init()
        {
            bulletPool = new Pool<Bullet> { container = bulletContainer };
        }

        public void LogicalUpdate()
        {
            List<Bullet> removeList = new();

            foreach (var bullet in bulletPool.Instances())
            {
                if (bullet.destroyFlag)
                {
                    removeList.Add(bullet);
                }
                else
                {
                    bullet.LogicalUpdate();
                }
            }

            foreach (var bullet in removeList)
            {
                bulletPool.Release(bullet);
            }
        }
    }
}
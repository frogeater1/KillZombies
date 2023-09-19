using System;
using System.Collections.Generic;
using KillZombies.Managers;
using UnityEngine;
using Utilities;

namespace KillZombies.Unit
{
    public class Weapon : MonoBehaviour
    {
        public Bullet bulletPrefab;
        public WeaponType type;
        public string action;
        public int shootTime;
        public int endTime;
        public Unit owner;
        public float range;
        public float speed;
        public bool hitOne; // 只打一个单位

        public void Init(Unit unit, int id)
        {
            owner = unit;

            Game.Instance.poolMgr.SetPool(prefab: bulletPrefab, defaultParent: transform, container: transform);

            if (id == 0)
            {
                type = WeaponType.Gun;
                action = "Character_Auto_SingleShot 0";
                shootTime = 6;
                endTime = 16;
                range = 15;
                speed = 20;
                hitOne = true;
            }
            else if (id == 1)
            {
                type = WeaponType.ZombieAttack;
                action = "Zombie_Attack";
                shootTime = 15;
                endTime = 35;
                range = 0;
                speed = 0;
                hitOne = false;
            }
        }

        public void SetTrans(FSMState state)
        {
            // switch (state)
            // {
            //     // case FSMState.Idle:
            //     // {
            //     //     var eulerAngle = transform.rotation.eulerAngles;
            //     //     transform.rotation = Quaternion.Euler(0, eulerAngle.y, eulerAngle.z);
            //     //     break;
            //     // }
            //     // case FSMState.Attack:
            //     // {
            //     //     var eulerAngle = transform.rotation.eulerAngles;
            //     //     transform.rotation = Quaternion.Euler(-45, eulerAngle.y, eulerAngle.z);
            //     //     break;
            //     // }
            //     // case FSMState.Death:
            //     //     break;
            // }
        }

        public void Shoot()
        {
            var bullet = Game.Instance.poolMgr.GetFromPool<Bullet>(bulletPrefab, transform.position, Quaternion.Euler(owner.transform.forward));
            bullet.Init(this);
        }

        public void LogicalUpdate()
        {
        }

        public void Work(Unit target)
        {
            Debug.Log(target.name + "hited");
        }
    }
}
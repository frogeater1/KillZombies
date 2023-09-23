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
        public float damage;

        public void Init(Unit unit, int id)
        {
            owner = unit;

            if (id == 0)
            {
                type = WeaponType.Gun;
                action = "Character_Auto_SingleShot 0";
                shootTime = 16;
                endTime = 32;
                range = 15;
                speed = 70;
                hitOne = true;
                damage = 10;
            }
            else if (id == 1)
            {
                type = WeaponType.ZombieAttack;
                action = "Zombie_Attack";
                shootTime = 30;
                endTime = 70;
                range = 0;
                speed = 0;
                hitOne = false;
                damage = 10;
            }
        }

        public void SetTrans(FSMState state)
        {
            // //调整枪的位置
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
            var bullet = Game.Instance.bulletPool.Get<Bullet>(bulletPrefab, transform.position, Quaternion.Euler(owner.transform.forward));
            bullet.Init(this);
        }

        public void LogicalUpdate()
        {
        }

        public void Work(Unit target)
        {
            target.Hited(this);
        }
    }
}
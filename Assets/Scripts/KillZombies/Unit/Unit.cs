using System;
using System.Collections.Generic;
using UnityEngine;

namespace KillZombies.Unit
{
    public abstract class Unit : MonoBehaviour
    {
        public Animator animator;
        public Rigidbody rb;
        public CapsuleCollider coll;


        public WeaponType curWeaponType;

        public List<Weapon> weaponList;

        public Dictionary<WeaponType, Weapon> weapons = new();


        // public readonly Dictionary<string, int> animLogicalFrameCount = new()
        // {
        //     { "Zombie_Attack", 35 },
        //     { "Character_Auto_SingleShot 0", 16 }
        // };

        public FSMCtrl fsmCtrl;

        public float gravity;

        public Vector3 direction;
        public float moveSpeed;


        public virtual void Awake()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            coll = GetComponent<CapsuleCollider>();
        }

        public virtual void Init()
        {
            gravity = -10f;

            foreach (var weapon in weaponList)
            {
                weapon.Init(this, weaponList.IndexOf(weapon));
                weapons.Add(weapon.type, weapon);
            }

            fsmCtrl = new FSMCtrl(this);
            fsmCtrl.Add(FSMState.Idle);
            fsmCtrl.Add(FSMState.Attack);
            fsmCtrl.Add(FSMState.Death);
            fsmCtrl.SwitchState(FSMState.Idle);
        }

        public void LogicalUpdate()
        {
            foreach (var weapon in weapons.Values)
            {
                weapon.LogicalUpdate();
            }

            fsmCtrl.Update();
        }

        public abstract void Move();

        public bool IsGrounded()
        {
            if (!Physics.Raycast(transform.position + coll.center, Vector3.down, out var hit))
            {
                Debug.LogError("不在地面正上方");
                return false;
            }

            return hit.transform.position.y >= transform.position.y;
        }

        public abstract void PlayAnim(FSMState state);
    }
}
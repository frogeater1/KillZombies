using System;
using System.Collections.Generic;
using KillZombies.UI;
using UnityEngine;

namespace KillZombies.Unit
{
    public abstract class Unit : MonoBehaviour
    {
        public Animator animator;
        public Rigidbody rb;
        public CapsuleCollider coll;

        public UI_HpSlider hpSlider;

        public WeaponType curWeaponType;

        public List<Weapon> weaponList;

        public Dictionary<WeaponType, Weapon> weapons = new();


        public FSMCtrl fsmCtrl;

        public float gravity;

        public Vector3 direction;
        public float moveSpeed;

        public float curHp;
        public float maxHp;


        public virtual void Awake()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            coll = GetComponent<CapsuleCollider>();
        }

        public virtual void Init()
        {
            gravity = -10f;

            fsmCtrl = new FSMCtrl(this);
            fsmCtrl.Add(FSMState.Idle);
            fsmCtrl.Add(FSMState.Attack);
            fsmCtrl.Add(FSMState.Death);
            fsmCtrl.SwitchState(FSMState.Idle);

            hpSlider = UI_HpSlider.CreateInstance();
            Game.Instance.uiMgr.uiMain.AddChild(hpSlider);
            hpSlider.Init(this);
            hpSlider.visible = true;
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
            if (!Physics.Raycast(transform.position + coll.center, Vector3.down, out var hit, 9999, 1 << LayerMask.NameToLayer("Ground")))
            {
                Debug.LogError("不在地面正上方");
                return false;
            }

            return hit.transform.position.y >= transform.position.y;
        }

        public abstract void PlayAnim(FSMState state);

        public void Hited(Weapon weapon)
        {
            Game.Instance.damageNumberPrefab.Spawn(transform.position, -weapon.damage);
            curHp -= weapon.damage;
            hpSlider.Refresh();
        }

        public virtual void Dead()
        {
            hpSlider.visible = false;
        }
    }
}
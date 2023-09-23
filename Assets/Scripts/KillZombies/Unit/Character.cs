using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace KillZombies.Unit
{
    public class Character : Unit
    {
        public Vector2 inputMovement;
        public float jumpStrength;


        public override void Init()
        {
            moveSpeed = 5f;
            jumpStrength = 5f;
            curHp = 100;
            maxHp = 100;

            foreach (var weapon in weaponList)
            {
                weapon.Init(this, 0);
                weapons.Add(weapon.type, weapon);
            }

            //tmp
            curWeaponType = WeaponType.Gun;

            base.Init();
        }

        public override void Move()
        {
            direction = Quaternion.Euler(0, Game.Instance.cameraMgr.mainCamera.transform.rotation.eulerAngles.y, 0) *
                        new Vector3(inputMovement.x, direction.y, inputMovement.y);
            if (!IsGrounded())
            {
                direction.y += gravity * Common.TickTime;
                animator.SetBool("Jump_b", direction.y > 0);
            }
            else
            {
                if (direction.y < 0)
                {
                    direction.y = 0;
                }

                //fixme: 落地有时会低于地面
                //落地的时候调整面向
                transform.forward = new Vector3(transform.forward.x, 0, transform.forward.z);
            }

            if (inputMovement != Vector2.zero && fsmCtrl.curState == FSMState.Idle)
            {
                transform.forward = new Vector3(direction.x, 0, direction.z);
            }

            rb.position = transform.position + moveSpeed * Common.TickTime * direction;
            animator.SetFloat("Speed_f", moveSpeed * direction.magnitude);
        }

        public override void PlayAnim(FSMState state)
        {
            switch (state)
            {
                case FSMState.Idle:
                    // weapons[curWeaponType].SetTrans(FSMState.Idle);
                    animator.CrossFadeInFixedTime("NoWeapon", 0.1f);
                    break;
                case FSMState.Attack:
                    // weapons[curWeaponType].SetTrans(FSMState.Attack);
                    animator.CrossFadeInFixedTime(weapons[curWeaponType].action, 0.1f);
                    break;
                case FSMState.Death:
                    animator.CrossFadeInFixedTime("Death", 0.1f);
                    break;
            }
        }


        public void Jump()
        {
            if (IsGrounded())
            {
                direction.y = jumpStrength;
            }
        }

        public void Fire()
        {
            if (fsmCtrl.curState == FSMState.Idle)
            {
                Debug.Log(Input.mousePosition);
                if (Physics.Raycast(Game.Instance.cameraMgr.mainCamera.ScreenPointToRay(Input.mousePosition), out var hit, 1000, LayerMask.GetMask("Ground")))
                {
                    transform.forward = hit.point - transform.position;
                }

                fsmCtrl.SwitchState(FSMState.Attack);
            }
        }
    }
}
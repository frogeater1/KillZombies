using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
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
            base.Init();
            moveSpeed = 5f;
            jumpStrength = 5f;

            //tmp
            curWeaponType = WeaponType.Gun;
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
                    direction.y = 0;
            }

            Turn();
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

        private void Turn()
        {
            if (inputMovement != Vector2.zero)
                transform.forward = new Vector3(direction.x, 0, direction.z);
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
            fsmCtrl.SwitchState(FSMState.Attack);
        }
    }
}
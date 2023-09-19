using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace KillZombies.Unit
{
    public class Zombie : Unit
    {
        public float seekRange;

        public override void Init()
        {
            base.Init();

            moveSpeed = 1.8f;
            seekRange = 15f;
        }

        public override void Move()
        {
            direction = Game.Instance.unitMgr.me.transform.position - transform.position;
            if (!IsGrounded())
            {
                direction.y += gravity * Common.TickTime;
            }
            else
            {
                if (direction.y < 0)
                    direction.y = 0;
            }

            var sqrDistance = direction.sqrMagnitude;

            if (sqrDistance <= seekRange * seekRange && sqrDistance > coll.radius * coll.radius)
            {
                Turn();
                rb.position = transform.position + moveSpeed * Common.TickTime * transform.forward.normalized;
                animator.SetFloat("Speed_f", moveSpeed);
            }
            else
            {
                animator.SetFloat("Speed_f", 0);
            }
        }

        public override void PlayAnim(FSMState state)
        {
            switch (state)
            {
                case FSMState.Idle:
                    animator.CrossFadeInFixedTime("Zombie_Idle", 0.1f);
                    break;
                case FSMState.Attack:
                    animator.CrossFadeInFixedTime(weapons[curWeaponType].action, 0.1f);
                    break;
                case FSMState.Death:
                    animator.CrossFadeInFixedTime("Zombie_Death", 0.1f);
                    break;
            }
        }


        private void Turn()
        {
            transform.forward = new Vector3(direction.x, 0, direction.z);
        }

        public bool CanAttack()
        {
            var sqrDistance = direction.sqrMagnitude;
            return sqrDistance <= coll.radius * coll.radius;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace KillZombies.Unit
{
    public class FSMCtrl
    {
        public FSMState curState;
        public Dictionary<FSMState, UnitState> states = new();
        public Unit unit;

        public FSMCtrl(Unit u)
        {
            unit = u;
        }

        public void Add(FSMState state)
        {
            UnitState s = state switch
            {
                FSMState.Idle => new IdleState(this),
                FSMState.Attack => new AttackState(this),
                FSMState.Death => new DeathState(this),
                _ => null
            };
            if (s != null)
            {
                states.Add(state, s);
            }
        }

        public void SwitchState(FSMState state, object data = null)
        {
            states[curState].OnExit();
            states[state].OnEnter(data);
            curState = state;
        }

        public void Update()
        {
            states[curState].Update();
        }
    }

    public abstract class UnitState
    {
        public FSMCtrl fsmCtrl;

        protected UnitState(FSMCtrl c)
        {
            fsmCtrl = c;
        }

        public abstract void OnEnter(object data);

        public virtual void Update()
        {
        }

        public abstract void OnExit();
    }

    public class IdleState : UnitState
    {
        public IdleState(FSMCtrl c) : base(c)
        {
        }

        public override void OnEnter(object data)
        {
            fsmCtrl.unit.PlayAnim(FSMState.Idle);
        }

        public override void Update()
        {
            //防止已经执行完旧状态OnExit后但还没执行完新状态的OnEnter时进入
            if (fsmCtrl.curState != FSMState.Idle)
            {
                Debug.Log("重入");
                return;
            }

            if (fsmCtrl.unit.curHp <= 0)
            {
                fsmCtrl.SwitchState(FSMState.Death);
                return;
            }

            fsmCtrl.unit.Move();

            if (fsmCtrl.unit is Zombie zombie)
            {
                //只有怪物会自动攻击
                if (zombie.CanAttack())
                {
                    fsmCtrl.SwitchState(FSMState.Attack);
                }
            }
        }

        public override void OnExit()
        {
        }
    }

    public class AttackState : UnitState
    {
        private Weapon _weapon;
        private int _endTime = 0;
        private int _shootTime = 0;
        private bool _shooted;

        public AttackState(FSMCtrl c) : base(c)
        {
        }

        public override void OnEnter(object data)
        {
            Debug.Log("attack");
            fsmCtrl.unit.PlayAnim(FSMState.Attack);
            _weapon = fsmCtrl.unit.weapons[fsmCtrl.unit.curWeaponType];
            _shootTime = _weapon.shootTime;
            _endTime = _weapon.endTime;
            _shooted = false;
        }

        public override void Update()
        {
            if (fsmCtrl.curState != FSMState.Attack)
            {
                Debug.Log("重入");
                return;
            }

            if (fsmCtrl.unit.curHp <= 0)
            {
                fsmCtrl.SwitchState(FSMState.Death);
                return;
            }


            if (fsmCtrl.unit is Character character)
            {
                //只有玩家可以移动攻击
                character.Move();
            }


            _shootTime--;
            _endTime--;

            if (_shooted == false && _shootTime <= 0)
            {
                _shooted = true;
                _weapon.Shoot();
            }

            if (_endTime <= 0)
            {
                fsmCtrl.SwitchState(FSMState.Idle);
            }
        }

        public override void OnExit()
        {
        }
    }

    public class DeathState : UnitState
    {
        public int endTime = 0;

        public DeathState(FSMCtrl c) : base(c)
        {
        }

        public override void OnEnter(object data)
        {
            endTime = 120;
            fsmCtrl.unit.PlayAnim(FSMState.Death);
        }

        public override void Update()
        {
            if (fsmCtrl.curState != FSMState.Death)
            {
                Debug.Log("重入");
                return;
            }

            endTime--;
            if (endTime <= 0)
            {
                fsmCtrl.unit.Dead();
            }
        }

        public override void OnExit()
        {
        }
    }
}
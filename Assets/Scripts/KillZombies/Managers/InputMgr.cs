using System.Collections;
using System.Collections.Generic;
using KillZombies;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KillZombies.Managers
{
    public class InputMgr : MonoBehaviour
    {
        void OnMove(InputValue value)
        {
            //转化为Vector3
            Vector2 input = value.Get<Vector2>();
            Game.Instance.unitMgr.me.inputMovement = input;
        }

        void OnLook(InputValue value)
        {
            Game.Instance.cameraMgr.Turn(value.Get<Vector2>().normalized);
        }

        void OnFire(InputValue value)
        {
            Game.Instance.unitMgr.me.Fire();
        }

        void OnJump(InputValue value)
        {
            Game.Instance.unitMgr.me.Jump();
        }


        void OnSpawnZombie(InputValue value)
        {
            Game.Instance.unitMgr.SpawnZombie();
        }


        void OnKillZombies(InputValue value)
        {
            Game.Instance.unitMgr.KillZombies();
        }
    }
}
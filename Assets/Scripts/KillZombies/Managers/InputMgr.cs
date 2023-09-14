using System.Collections;
using System.Collections.Generic;
using KillZombies;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMgr : MonoBehaviour
{
    void OnMove(InputValue value)
    {
        //转化为Vector3
        Vector2 input = value.Get<Vector2>();
        Debug.Log(input);
        Game.Instance.character.inputMovement = input;
    }

    void OnLook(InputValue value)
    {
        Game.Instance.cameraMgr.Turn(value.Get<Vector2>());
    }

    void OnFire(InputValue value)
    {
        Debug.Log("Fire");
    }

    void OnJump(InputValue value)
    {
        Game.Instance.character.Jump();
    }
}
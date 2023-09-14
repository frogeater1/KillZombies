using System.Collections;
using System.Collections.Generic;
using KillZombies;
using KillZombies.Unit;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Character character;

    // Update is called once per frame
    void Update()
    {
        var cameraForward = Game.Instance.cameraMgr.virtualCamera.transform.forward;

        transform.position = character.transform.position - new Vector3(cameraForward.x, 0, cameraForward.z) * 15;
    }
}
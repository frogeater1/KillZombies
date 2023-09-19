using System.Collections;
using System.Collections.Generic;
using KillZombies;
using KillZombies.Unit;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    private const float Distance = 20f; //相机跟随目标距离人物的平面距离

    void Update()
    {
        var cameraForward = Game.Instance.cameraMgr.virtualCamera.transform.forward;

        transform.position = Game.Instance.unitMgr.me.transform.position -
                             new Vector3(cameraForward.x, 0, cameraForward.z) * Distance;
    }
}
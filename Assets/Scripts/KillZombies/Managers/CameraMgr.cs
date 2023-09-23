using Cinemachine;
using UnityEngine;

namespace KillZombies.Managers
{
    public class CameraMgr : MonoBehaviour
    {
        public Camera mainCamera;
        public CinemachineVirtualCamera virtualCamera;

        private float yaw = 0;

        private float pitch = 0;

        public void Init()
        {
            mainCamera = Camera.main;
            virtualCamera = FindFirstObjectByType<CinemachineVirtualCamera>().GetComponent<CinemachineVirtualCamera>();
        }

        public void Turn(Vector2 offset)
        {
            var euler = virtualCamera.transform.eulerAngles;
            yaw = offset.x * 0.5f;
            pitch = -offset.y * 0.5f;

            virtualCamera.transform.rotation =
                Quaternion.Euler(Mathf.Clamp(euler.x + pitch, 0, 89f), euler.y + yaw,
                    euler.z);
        }
    }
}
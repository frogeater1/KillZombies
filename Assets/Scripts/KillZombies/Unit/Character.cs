using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace KillZombies.Unit
{
    public class Character : Unit
    {
        public CharacterController controller;
        public Vector2 inputMovement;
        public Animator animator;

        public Vector3 direction;

        private float moveSpeed = 5f;
        private float gravity = -600f;
        private float jumpStrength = 400f;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            direction = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) *
                        new Vector3(inputMovement.x, 0, inputMovement.y);
            // if (!controller.isGrounded)
            // {
            //     movement.y = gravity * Time.deltaTime;
            // }

            //向右旋转60度
            Turn();
            controller.Move(moveSpeed * Time.deltaTime * direction);
            animator.SetFloat("Speed_f", inputMovement.magnitude * moveSpeed);
        }

        private void Turn()
        {
            if (inputMovement != Vector2.zero)
                transform.forward = new Vector3(direction.x, 0, direction.z);
        }

        public void Jump()
        {
            Debug.Log("Jump");
            if (controller.isGrounded)
            {
                Debug.Log("isGrounded");
                direction.y = jumpStrength;
            }
        }
    }
}
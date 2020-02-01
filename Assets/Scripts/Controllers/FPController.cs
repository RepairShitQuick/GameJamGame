using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class FPController : MonoBehaviour
    {
        public float Speed = 10.0f;
        public float JumpForce = 50.0f;
        private Rigidbody _rigidbody;
        private float _distanceToGround;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _rigidbody = GetComponent<Rigidbody>();
            _distanceToGround = GetComponent<Collider>().bounds.extents.y;
        }

        void Update()
        {
            var forwardTranslation = Input.GetAxis("Vertical") * Speed;
            var strafeTranslation = Input.GetAxis("Horizontal") * Speed;
        
            Move(forwardTranslation, strafeTranslation);
            

            if (IsGrounded() && Input.GetButton("Jump"))
            {
                Jump();
            }

            if (Input.GetKey("escape"))
            {
                UnlockCursor();
            }
        }

        private void OnCollisionExit(Collision other)
        {
            _rigidbody.velocity = new Vector3(0f, _rigidbody.velocity.y, 0f);
            _rigidbody.angularVelocity = Vector3.zero;
        }

        private void Move(float forwardTranslation, float straffeTranslation)
        {
            forwardTranslation *= Time.deltaTime;
            straffeTranslation *= Time.deltaTime;
        
            transform.Translate(straffeTranslation, 0, forwardTranslation);
        }

        private void Jump()
        {
            _rigidbody.AddForce(Vector3.up * JumpForce);
        }

        private bool IsGrounded()
        {
            return Physics.Raycast(transform.position, -Vector3.up, _distanceToGround + 0.1f);
        }

        private void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
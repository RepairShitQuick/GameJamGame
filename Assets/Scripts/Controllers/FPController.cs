using System;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class FPController : MonoBehaviour
    {
        private float _speed = 10f;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            var forwardTranslation = Input.GetAxis("Vertical") * _speed;
            var strafeTranslation = Input.GetAxis("Horizontal") * _speed;
        
            Move(forwardTranslation, strafeTranslation);

            if (Input.GetKey("escape"))
            {
                UnlockCursor();
            }
        }

        private void OnCollisionExit(Collision other)
        {
            var rigidBody = GetComponent<Rigidbody>();
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
        }

        private void Move(float forwardTranslation, float straffeTranslation)
        {
            forwardTranslation *= Time.deltaTime;
            straffeTranslation *= Time.deltaTime;
        
            transform.Translate(straffeTranslation, 0, forwardTranslation);
        }

        private void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
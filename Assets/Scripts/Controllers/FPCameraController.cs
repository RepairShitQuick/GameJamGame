using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class FPCameraController : MonoBehaviour
    {
        public enum RotationAxes {MouseX = 0, MouseY = 1 }

        public RotationAxes AxisToRotate = RotationAxes.MouseY;

        private float _sensitivityY = 10.0f;
        public float MaximumY = 60.0f;
        public float MinimumY = -60.0f;
        public float RotationY = 0.0f;

        private float _sensitivityX = 10.0f;
        public float MaximumX = 360.0f;
        public float MinimumX = -360.0f;
        public float RotationX = 0.0f;
        
        private Quaternion _origionalRotation;
        void Start()
        {
            _origionalRotation = transform.localRotation;
        }

        private void Update()
        {
            if (AxisToRotate == RotationAxes.MouseY)
            {
                RotationY += Input.GetAxis("Mouse Y") * _sensitivityY;
                RotationY = ClampAngle(RotationY, MinimumY, MaximumY);
                var quaternionY = Quaternion.AngleAxis(-RotationY, Vector3.right);
                transform.localRotation = _origionalRotation * quaternionY;
            }
            else
            {
                RotationX += Input.GetAxis("Mouse X") * _sensitivityX;
                RotationX = ClampAngle(RotationX, MinimumX, MaximumX);
                var quaternionX = Quaternion.AngleAxis(RotationX, Vector3.up);
                transform.localRotation = _origionalRotation * quaternionX;
            }
        }
        
        public static float ClampAngle (float angle, float min, float max)
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
            return Mathf.Clamp (angle, min, max);
        }
    }
}
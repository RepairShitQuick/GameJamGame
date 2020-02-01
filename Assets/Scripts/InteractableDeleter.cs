using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Behaviors;
using UnityEngine;

namespace Assets.Scripts
{
    public class InteractableDeleter : MonoBehaviour
    {
        private Camera _playerCamera;
        private Vector3 _screenForward;

        void Start()
        {
            _playerCamera = GetComponent<Camera>();
            _screenForward = new Vector3(0.5f, 0.5f);
        }

        void Update()
        {

            Debug.DrawRay(_playerCamera.ViewportPointToRay(_screenForward).origin, _playerCamera.ViewportPointToRay(_screenForward).direction, Color.green);
            if (Physics.Raycast(_playerCamera.ViewportPointToRay(_screenForward), out var info, maxDistance: 5f))
            {
                var interactable = info.collider.gameObject.GetComponent<HullBreachInteractable>();
                if (interactable != null && Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
        }
    }
}

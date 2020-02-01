using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Behaviors;
using UnityEngine;

public class InteractableViewer : MonoBehaviour
{
    private Camera _playerCamera;
    private List<KeyValuePair<GameObject, Material>> _gameObjectsToReset;
    private Vector3 _screenForward;
    public Material MaterialToSet;

    void Start()
    {
        _gameObjectsToReset = new List<KeyValuePair<GameObject, Material>>();
        _playerCamera = GetComponent<Camera>();
        _screenForward = new Vector3(0.5f, 0.5f);
    }

    void Update()
    {
        for (int i = 0; i < _gameObjectsToReset.Count; i++)
        {
            if (_gameObjectsToReset[i].Key != null)
            {
                var renderer = _gameObjectsToReset[i].Key.GetComponent<Renderer>();
                renderer.material = _gameObjectsToReset[i].Value;
            }
        }
        _gameObjectsToReset.Clear();
        Debug.DrawRay(_playerCamera.ViewportPointToRay(_screenForward).origin, _playerCamera.ViewportPointToRay(_screenForward).direction, Color.green);
        if (Physics.Raycast(_playerCamera.ViewportPointToRay(_screenForward), out var info, maxDistance: 5f))
        {
            var interactable = info.collider.gameObject.GetComponent<HullBreachInteractable>();
            if (interactable != null)
            {
                var renderer =
                    interactable.GetComponent<Renderer>();
                _gameObjectsToReset.Add(new KeyValuePair<GameObject, Material>(interactable.gameObject, renderer.material));
                renderer.material = MaterialToSet;
            }
        }
    }
}

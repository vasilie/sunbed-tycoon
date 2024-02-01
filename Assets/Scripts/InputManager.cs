using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Camera sceneCamera;
    private Vector3 lastPosition;

    public event Action OnClicked, OnExit;

    private void Update() {
        if (Input.GetMouseButtonDown(0)){
            OnClicked?.Invoke();
        }        
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)){
            OnExit?.Invoke();
        }
    }

    public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();

    [SerializeField]
    private LayerMask placementLayerMask;

    public Vector3 GetSelectedMapPosition() {
        Vector3 mousePos = Input.mousePosition;
        // mousePos.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500, placementLayerMask)) {
            lastPosition = hit.point;
        }

        return lastPosition;
    }

}

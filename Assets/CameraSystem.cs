using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSystem : MonoBehaviour
{
    public float moveSpeed = 50f;
    public float dragPanSpeed = 0.2f;
    public float rotateSpeed = 50f;
    public float zoomSpeed = 14f;
    public float zoomAmount = 3f;
    public float targetFieldOfView = 50f;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private bool useEdgeScrolling = false;
    [SerializeField] private bool useDragPan = false;
    [SerializeField] private float fovMax = 50;
    [SerializeField] private float fovMin = 10;
    [SerializeField] private float followOffsetMax = 50;
    [SerializeField] private float followOffsetMin = 10;
    [SerializeField] private Vector2 boundsBottomLeftCornerPosition;
    [SerializeField] private float boundsRectangleWidth;
    [SerializeField] private float boundsRectangleHeight;
    private bool dragAndMoveActive = false;
    private Vector2 lastMousePosition;
    private Vector3 followOffset;

    void Awake()
    {
        followOffset = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
    }

    private void Update()
    {
        HandleCameraMovement();
        if (useEdgeScrolling){
            HandleCameraMovementEdgeScrolling();
        }        
        if (useDragPan){
            HandleCameraMovementDragPan();
        }
        HandleCameraRotation();
        HandleCameraZoom_MoveForward();
    }

    private void HandleCameraMovement() {
        Vector3 inputDir = new Vector3(0, 0, 0);

        //Move camera
        if (Input.GetKey(KeyCode.W)) inputDir.z = +1f;
        if (Input.GetKey(KeyCode.S)) inputDir.z = -1f;
        if (Input.GetKey(KeyCode.A)) inputDir.x = -1f;
        if (Input.GetKey(KeyCode.D)) inputDir.x = +1f;

        UpdatePosition(inputDir);
    }    

    private void HandleCameraMovementEdgeScrolling() {
        Vector3 inputDir = new Vector3(0, 0, 0);
        //Edge camera scroll 
        int edgeScrollSize = 20;
        if (Input.mousePosition.x < edgeScrollSize) inputDir.x = -1f;
        if (Input.mousePosition.y < edgeScrollSize) inputDir.z = -1f;
        if (Input.mousePosition.x > Screen.width - edgeScrollSize) inputDir.x = +1f;
        if (Input.mousePosition.y > Screen.height - edgeScrollSize) inputDir.z = +1f;
        
        UpdatePosition(inputDir);
    }

    private void HandleCameraMovementDragPan() {
        Vector3 inputDir = new Vector3(0, 0, 0);

        if(Input.GetMouseButtonDown(1)){
            dragAndMoveActive = true;
            lastMousePosition = Input.mousePosition;
        }
        if(Input.GetMouseButtonUp(1)){
            dragAndMoveActive = false;
        }
        if (dragAndMoveActive) {
            Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - lastMousePosition;

            inputDir.x = -mouseMovementDelta.x * dragPanSpeed;
            inputDir.z = -mouseMovementDelta.y * dragPanSpeed;

            lastMousePosition = Input.mousePosition;
        }

        UpdatePosition(inputDir);
    }
    
    private void HandleCameraRotation() {
        //Rotate camera
        float rotateDir = 0f;

        if (Input.GetKey(KeyCode.Q)) rotateDir +=1;
        if (Input.GetKey(KeyCode.E)) rotateDir -=1;

        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed * Time.deltaTime, 0);
    }    

    private void HandleCameraZoom() {
        if (Input.mouseScrollDelta.y > 0) {
            targetFieldOfView -= 5;
        }    
        if (Input.mouseScrollDelta.y < 0) {
            targetFieldOfView += 5;
        }
        targetFieldOfView = Mathf.Clamp(targetFieldOfView, fovMin, fovMax);
        cinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(cinemachineVirtualCamera.m_Lens.FieldOfView, targetFieldOfView, Time.deltaTime * zoomSpeed);
    }

    private void HandleCameraZoom_MoveForward() {
        Vector3 zoomDir = followOffset.normalized;

        if (Input.mouseScrollDelta.y > 0) {
            followOffset-= zoomDir * zoomAmount;
        }    
        if (Input.mouseScrollDelta.y < 0) {
            followOffset+= zoomDir * zoomAmount;
        }   
        if (followOffset.magnitude < followOffsetMin) {
            followOffset = zoomDir * followOffsetMin;
        }
        if (followOffset.magnitude > followOffsetMax) {
            followOffset = zoomDir * followOffsetMax;
        }
       
        targetFieldOfView = Mathf.Clamp(targetFieldOfView, fovMin, fovMax);
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
            Vector3.Lerp(cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, followOffset, Time.deltaTime * zoomSpeed);
    }

    private bool CheckIfInBounds(Vector3 position) => (
            position.x > boundsBottomLeftCornerPosition.x &&
            position.x < boundsBottomLeftCornerPosition.x + boundsRectangleWidth &&
            position.z > boundsBottomLeftCornerPosition.y &&
            position.z < boundsBottomLeftCornerPosition.y + boundsRectangleHeight
        );

    private void UpdatePosition(Vector3 inputDir) {
        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
        moveDir *= (moveSpeed + 20f) * Time.deltaTime;

        if (CheckIfInBounds(transform.position + moveDir)) {
            transform.position += moveDir;
        } else if (CheckIfInBounds(transform.position + new Vector3(moveDir.x, 0, 0))) {
            transform.position += new Vector3(moveDir.x, 0, 0);
        } else if (CheckIfInBounds(transform.position + new Vector3(0, 0, moveDir.z))) {
            transform.position += new Vector3(0, 0, moveDir.z);
        }
    }
}

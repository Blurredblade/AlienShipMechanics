using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Serializable]
    public class CameraSettings{
        [Range(1f, 20f)] public float xSensetivity = 2f;
        [Range(1f, 20f)] public float ySensetivity = 2f;
        public float xMin = -90f;
        public float xMax = 90f;
        public float smoothTime = 5f;
    }
    public float walkSpeed = 5f;
    private Vector2 input;
    [SerializeField] private CameraSettings _cameraSettings;
    private Camera _camera;
    private Quaternion playerRot;
    private Quaternion cameraRot;
    private CharacterController _characterController;
    
    void Start()
    {
        _camera = transform.GetChild(0).GetComponent<Camera>();
        _characterController = GetComponent<CharacterController>();
        playerRot = transform.localRotation;
        cameraRot = _camera.transform.localRotation;
        SetCursorState(CursorLockMode.Locked, false);
    }

    void Update()
    {
        AdjustRotation();

    }

    void FixedUpdate(){
        float hor = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        input = new Vector2(hor, vert);
        if((input.x > 0) && (input.y > 0)){
            input.Normalize();
        }
        
        
        Vector3 move = transform.forward * input.y + transform.right * input.x;

        _characterController.Move(move * walkSpeed * Time.deltaTime);
    }

    void SetCursorState(CursorLockMode mode, bool visible){
        Cursor.lockState = mode;
        Cursor.visible = visible;
    }

    void AdjustRotation(){
        float yRot = Input.GetAxis("Mouse X") * _cameraSettings.xSensetivity * Time.deltaTime * 15f;
        float xRot = Input.GetAxis("Mouse Y") * _cameraSettings.ySensetivity * Time.deltaTime * 15f;

        cameraRot *= Quaternion.Euler(-xRot, 0, 0);
        playerRot *= Quaternion.Euler(0, yRot, 0);

        _camera.transform.localRotation = cameraRot;
        transform.localRotation = playerRot;
    }

    
}

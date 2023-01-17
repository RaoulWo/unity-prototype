using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /* ---- INPUT SYSTEM ---- */
    private PlayerInputActions _playerInputActions;
    private InputAction _move;
    private InputAction _look;
    private InputAction _jump;
    
    /* ---- PLAYER LOOK ---- */
    [Header("PLAYER LOOK")]
    [SerializeField] private float lookSensitivity = 90f;
    [SerializeField] private float headUpperAngleLimit = 85f;
    [SerializeField] private float headLowerAngleLimit = -80f;

    private Transform _head;
    private float _yaw = 0f;
    private float _pitch = 0f;
    private Quaternion _bodyStartOrientation;
    private Quaternion _headStartOrientation;
    
    private void Awake()
    {
        // Instantiate a new instance of the input action asset
        _playerInputActions = new PlayerInputActions();
        
        // Instantiate _head
        _head = GetComponentInChildren<Camera>().transform;
        
        // Cache orientation of body and head
        _bodyStartOrientation = transform.localRotation;
        _headStartOrientation = _head.localRotation;
        
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        // Handle player look
        Look();
        // Handle player movement
        
    }

    private void OnEnable()
    {
        // Cache and enable Move, Look and Jump actions
        _move = _playerInputActions.Player.Move;
        _look = _playerInputActions.Player.Look;
        _jump = _playerInputActions.Player.Jump;
        _move.Enable();
        _look.Enable();
        _jump.Enable();
        
        // Subscribe to all events
        _jump.performed += OnJump;
    }

    private void OnDisable()
    {
        // Disable the Move, Look and Jump actions
        _move.Disable();
        _look.Disable();
        _jump.Disable();
        
        // Unsubscribe to all events
        _jump.performed -= OnJump;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("OnJump is called");
    }

    private void Look()
    {
        // Cache the value of the Look action
        var lookDir = _look.ReadValue<Vector2>();

        // Scale the movement based on sensitivity and elapsed time
        var horizontal = lookDir.x * Time.deltaTime * lookSensitivity;
        var vertical = -lookDir.y * Time.deltaTime * lookSensitivity;

        // Update the _yaw and _pitch values
        _yaw += horizontal;
        _pitch += vertical;

        // Clamp _pitch
        _pitch = Mathf.Clamp(_pitch, headLowerAngleLimit, headUpperAngleLimit);

        // Compute a rotation for the body by a number of _yaw degrees around the y-axis
        var bodyRotation = Quaternion.AngleAxis(_yaw, Vector3.up);
        // Compute a rotation for the head by a number of _pitch degrees around the x-axis
        var headRotation = Quaternion.AngleAxis(_pitch, Vector3.right);

        // Create new rotations by combining them with their starting rotations
        transform.localRotation = bodyRotation * _bodyStartOrientation;
        _head.localRotation = headRotation * _headStartOrientation;
    }

    private void Move()
    {
        throw new NotImplementedException();
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class WalkingState : GroundedState
{
    // The cached value of the input action Move
    private Vector2 _movementInput;

    public WalkingState(Player player, PlayerInputActions playerInputActions) 
        : base(player, playerInputActions)
    { }
    
    public override void Enter()
    {
        Debug.Log("Enter WalkingState");

        PlayerInputActions.Player.Crouch.performed += OnCrouch;
        PlayerInputActions.Player.Run.performed += OnRun;
        
        // Call Enter method of GroundedState
        base.Enter();
    }

    public override void HandleInput()
    {
        // Store value of the input action Move
        _movementInput = PlayerInputActions.Player.Move.ReadValue<Vector2>();
    }

    public override void LogicUpdate()
    {
        if (Player.Controller.velocity == Vector3.zero)
        {
            Player.ChangeState(Player.StandingState);
        }
    }

    public override void PhysicsUpdate()
    {
        // Calculate the movement vector
        var movementVector = new Vector3(_movementInput.x, 0f, _movementInput.y) * (Player.WalkingSpeed * Time.deltaTime);
        
        // Transform the vector from local to global coordinates
        movementVector = Player.transform.TransformDirection(movementVector);

        // Move the player controller
        Player.Controller.Move(movementVector);
    }

    public override void Exit()
    {
        Debug.Log("Exit WalkingState");

        PlayerInputActions.Player.Crouch.performed -= OnCrouch;
        PlayerInputActions.Player.Run.performed -= OnRun;
        
        // Call Exit method of GroundedState
        base.Exit();
    }

    private void OnCrouch(InputAction.CallbackContext context)
    {
        Debug.Log("OnCrouch is called");

        if (_movementInput != Vector2.zero)
        {
            Player.ChangeState(Player.SneakingState);
        }
        else
        {
            Player.ChangeState(Player.CrouchingState);
        }
    }

    private void OnRun(InputAction.CallbackContext context)
    {
        Debug.Log("OnRun is called");

        if (_movementInput != Vector2.zero)
        {
            Player.ChangeState(Player.RunningState);
        }
    }
}

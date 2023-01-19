using UnityEngine;
using UnityEngine.InputSystem;

public class SneakingState : GroundedState
{
    // The cached value of the input action Move
    private Vector2 _movementInput;
    
    public SneakingState(Player player, PlayerInputActions playerInputActions) 
        : base(player, playerInputActions)
    { }
    
    public override void Enter()
    {
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
            Player.ChangeState(Player.CrouchingState);
        }
    }

    public override void PhysicsUpdate()
    {
        // Calculate the movement vector
        var movementVector = new Vector3(_movementInput.x, 0f, _movementInput.y) * (Player.SneakingSpeed * Time.deltaTime);
        
        // Transform the vector from local to global coordinates
        movementVector = Player.transform.TransformDirection(movementVector);

        // Move the player controller
        Player.Controller.Move(movementVector);
    }

    public override void Exit()
    {
        PlayerInputActions.Player.Crouch.performed -= OnCrouch;
        PlayerInputActions.Player.Run.performed -= OnRun;

        // Call Exit method of GroundedState
        base.Exit();
    }

    private void OnCrouch(InputAction.CallbackContext context)
    {
        if (_movementInput != Vector2.zero)
        {
            Player.ChangeState(Player.WalkingState);
        }
        else
        {
            Player.ChangeState(Player.StandingState);
        }
    }

    private void OnRun(InputAction.CallbackContext context)
    {
        if (_movementInput != Vector2.zero)
        {
            Player.ChangeState(Player.RunningState);
        }
    }
}

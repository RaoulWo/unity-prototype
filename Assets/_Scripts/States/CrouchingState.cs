using UnityEngine;
using UnityEngine.InputSystem;

public class CrouchingState : GroundedState
{
    // The cached value of the input action Move
    private Vector2 _movementInput;
    
    public CrouchingState(IPlayer player) 
        : base(player)
    { }
    
    public override void Enter()
    {
        Player.PlayerInputActions.Player.Crouch.performed += OnCrouch;
        Player.PlayerInputActions.Player.Run.performed += OnRun;

        // Call Enter method of GroundedState
        base.Enter();
    }

    public override void HandleInput()
    {
        // Store value of the input action Move
        _movementInput = Player.PlayerInputActions.Player.Move.ReadValue<Vector2>();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (_movementInput != Vector2.zero)
        {
            Player.ChangeState(Player.SneakingState);
        }
    }

    public override void PhysicsUpdate()
    {
        // Calculate the movement vector
        var movementVector = new Vector3(0, -Player.Gravity * Time.deltaTime, 0);
        
        // Transform the vector from local to global coordinates
        movementVector = Player.Transform.TransformDirection(movementVector);

        // Move the player controller
        Player.Controller.Move(movementVector);
    }

    public override void Exit()
    {
        Player.PlayerInputActions.Player.Crouch.performed -= OnCrouch;
        Player.PlayerInputActions.Player.Run.performed -= OnRun;
        
        // Call Exit method of GroundedState
        base.Exit();
    }

    private void OnCrouch(InputAction.CallbackContext context)
    {
        if (_movementInput == Vector2.zero)
        {
            Player.ChangeState(Player.StandingState);
        }
        else
        {
            Player.ChangeState(Player.WalkingState);
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

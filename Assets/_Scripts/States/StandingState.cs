using UnityEngine;
using UnityEngine.InputSystem;

public class StandingState : GroundedState
{
    // The cached value of the input action Move
    private Vector2 _movementInput;

    public StandingState(Player player, PlayerInputActions playerInputActions) 
        : base(player, playerInputActions)
    { }
    
    public override void Enter()
    {
        Debug.Log("Enter StandingState");

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
        if (_movementInput != Vector2.zero)
        {
            Player.ChangeState(Player.WalkingState);
        }
    }

    public override void PhysicsUpdate()
    {

    }

    public override void Exit()
    {
        Debug.Log("Exit StandingState");

        PlayerInputActions.Player.Crouch.performed -= OnCrouch;
        PlayerInputActions.Player.Run.performed -= OnRun;
        
        // Call Exit method of GroundedState
        base.Exit();
    }

    private void OnCrouch(InputAction.CallbackContext context)
    {
        Debug.Log("OnCrouch is called");

        if (_movementInput == Vector2.zero)
        {
            Player.ChangeState(Player.CrouchingState);
        }
        else
        {
            Player.ChangeState(Player.SneakingState);
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

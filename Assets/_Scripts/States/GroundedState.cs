using UnityEngine;
using UnityEngine.InputSystem;

public class GroundedState : PlayerState
{
    public GroundedState(Player player, PlayerInputActions playerInputActions) 
        : base(player, playerInputActions)
    { }
    
    public override void Enter()
    {
        Debug.Log("Enter GroundedState");

        PlayerInputActions.Player.Jump.performed += OnJump;
    }

    public override void HandleInput()
    {
        
    }

    public override void LogicUpdate()
    {
        
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void Exit()
    {
        Debug.Log("Exit GroundedState");

        PlayerInputActions.Player.Jump.performed -= OnJump;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("OnJump is called");
    }
}

using UnityEngine.InputSystem;

public abstract class GroundedState : PlayerState
{
    protected GroundedState(IPlayer player) 
        : base(player)
    { }
    
    public override void Enter()
    {
        Player.PlayerInputActions.Player.Jump.performed += OnJump;
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
        Player.PlayerInputActions.Player.Jump.performed -= OnJump;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        Player.ChangeState(Player.JumpingState);
    }
}

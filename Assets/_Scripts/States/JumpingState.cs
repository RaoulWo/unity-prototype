using UnityEngine;

public class JumpingState : PlayerState
{
    public JumpingState(Player player, PlayerInputActions playerInputActions) 
        : base(player, playerInputActions)
    { }
    
    public override void Enter()
    {
        Debug.Log("Enter JumpingState");
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
        Debug.Log("Exit JumpingState");
    }
}

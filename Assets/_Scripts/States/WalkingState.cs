using UnityEngine;

public class WalkingState : GroundedState
{
    public WalkingState(Player player, PlayerInputActions playerInputActions) 
        : base(player, playerInputActions)
    { }
    
    public override void Enter()
    {
        Debug.Log("Enter WalkingState");
        
        // Call Enter method of GroundedState
        base.Enter();
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
        Debug.Log("Exit WalkingState");
        
        // Call Exit method of GroundedState
        base.Exit();
    }
}

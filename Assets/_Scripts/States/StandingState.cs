using UnityEngine;

public class StandingState : GroundedState
{
    public StandingState(Player player, PlayerInputActions playerInputActions) 
        : base(player, playerInputActions)
    { }
    
    public override void Enter()
    {
        Debug.Log("Enter StandingState");

        // Call Enter method of GroundedState
        base.Enter();
    }

    public override void HandleInput()
    {
        var movementInput = PlayerInputActions.Player.Move.ReadValue<Vector2>();
    }

    public override void LogicUpdate()
    {
        
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void Exit()
    {
        Debug.Log("Exit StandingState");
        
        // Call Exit method of GroundedState
        base.Exit();
    }
}

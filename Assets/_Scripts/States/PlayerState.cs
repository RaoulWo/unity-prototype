public class PlayerState
{
    protected readonly Player Player;
    protected readonly PlayerInputActions PlayerInputActions;

    protected PlayerState(Player player, PlayerInputActions playerInputActions)
    {
        Player = player;
        PlayerInputActions = playerInputActions;
    }

    public virtual void Enter()
    {
        
    }

    public virtual void HandleInput()
    {
        
    }

    public virtual void LogicUpdate()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        
    }

    public virtual void Exit()
    {
        
    }
}

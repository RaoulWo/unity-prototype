public class PlayerState
{
    protected Player Player;
    protected PlayerInputActions PlayerInputActions;

    public PlayerState(Player player, PlayerInputActions playerInputActions)
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

public class PlayerState
{
    protected readonly IPlayer Player;

    protected PlayerState(IPlayer player)
    {
        Player = player;
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

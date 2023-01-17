public class JumpingState : IState
{
    public void Enter(Player player, PlayerInputActions playerInputActions)
    {
        throw new System.NotImplementedException();
        // TODO Subscribe to all input action events needed
        // TODO Handle animations via events/observer pattern?
    }

    public void HandleInput(Player player, PlayerInputActions playerInputActions)
    {
        throw new System.NotImplementedException();
    }

    public void LogicUpdate(Player player)
    {
        throw new System.NotImplementedException();
    }

    public void PhysicsUpdate(Player player)
    {
        throw new System.NotImplementedException();
    }

    public void Exit(Player player, PlayerInputActions playerInputActions)
    {
        throw new System.NotImplementedException();
        // TODO Unsubscribe to all input action events needed
    }
}

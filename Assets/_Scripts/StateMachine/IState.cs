public interface IState
{
    void Enter(Player player, PlayerInputActions playerInputActions);
    void HandleInput(Player player, PlayerInputActions playerInputActions);
    void LogicUpdate(Player player);
    void PhysicsUpdate(Player player);
    void Exit(Player player, PlayerInputActions playerInputActions);
}

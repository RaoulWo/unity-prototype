using UnityEngine;

public class Player : MonoBehaviour
{
    /* ---- INPUT SYSTEM ---- */
    private PlayerInputActions _playerInputActions;

    /* ---- PLAYER STATES ---- */
    private PlayerState _playerState;

    public JumpingState JumpingState;
    public StandingState StandingState;
    public WalkingState WalkingState;

    #region "MonoBehaviour event functions"
    private void Awake()
    {
        // Instantiate a new instance of the input action asset
        _playerInputActions = new PlayerInputActions();
        
        // Instantiate the player states
        JumpingState = new JumpingState(this, _playerInputActions);
        StandingState = new StandingState(this, _playerInputActions);
        WalkingState = new WalkingState(this, _playerInputActions);

        // Initialize the player in StandingState
        InitializeState(StandingState);
    }

    private void Update()
    {
        _playerState.HandleInput();
        _playerState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        _playerState.PhysicsUpdate();
    }

    private void OnEnable()
    {
        // Enable Move and Jump actions
        _playerInputActions.Player.Move.Enable();
        _playerInputActions.Player.Jump.Enable();
    }

    private void OnDisable()
    {
        // Disable the Move and Jump actions
        _playerInputActions.Player.Move.Disable();
        _playerInputActions.Player.Jump.Disable();
        
        // TODO Here ??? Unsubscribe to all events
    }
    #endregion

    private void InitializeState(PlayerState startingPlayerState)
    {
        _playerState = startingPlayerState;
        _playerState.Enter();
    }

    public void ChangeState(PlayerState newPlayerState)
    {
        _playerState.Exit();

        _playerState = newPlayerState;
        _playerState.Enter();
    }
}

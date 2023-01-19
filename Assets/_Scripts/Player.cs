using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController Controller => _controller;
    private CharacterController _controller;
    
    private PlayerInputActions _playerInputActions;

    public float AirControl => airControl;
    public float Gravity => gravity;
    public float JumpHeight => jumpHeight;
    public float RunningSpeed => runningSpeed;
    public float SneakingSpeed => sneakingSpeed;
    public float WalkingSpeed => walkingSpeed;

    [SerializeField] private float airControl = 5f;
    [SerializeField] private float gravity = 10f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float runningSpeed = 10f;
    [SerializeField] private float sneakingSpeed = 2f;
    [SerializeField] private float walkingSpeed = 5f;
    
    public PlayerState CurrentState => _currentState;
    public PlayerState PreviousState => _previousState;
    private PlayerState _currentState;
    private PlayerState _previousState;

    public CrouchingState CrouchingState;
    public JumpingState JumpingState;
    public RunningState RunningState;
    public SneakingState SneakingState;
    public StandingState StandingState;
    public WalkingState WalkingState;

    private void Awake()
    {
        // Cache a reference to the CharacterController
        _controller = GetComponent<CharacterController>();

        // Instantiate a new instance of the input action asset
        _playerInputActions = new PlayerInputActions();
        
        // Instantiate the player states
        CrouchingState = new CrouchingState(this, _playerInputActions);
        JumpingState = new JumpingState(this, _playerInputActions);
        RunningState = new RunningState(this, _playerInputActions);
        SneakingState = new SneakingState(this, _playerInputActions);
        StandingState = new StandingState(this, _playerInputActions);
        WalkingState = new WalkingState(this, _playerInputActions);

        // Initialize the player in StandingState
        InitializeState(StandingState);
    }

    private void Update()
    {
        _currentState.HandleInput();
        _currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        _currentState.PhysicsUpdate();
    }

    private void OnEnable()
    {
        // Enable the input actions
        _playerInputActions.Player.Move.Enable();
        _playerInputActions.Player.Jump.Enable();
        _playerInputActions.Player.Crouch.Enable();
        _playerInputActions.Player.Run.Enable();
    }

    private void OnDisable()
    {
        // Disable the input actions
        _playerInputActions.Player.Move.Disable();
        _playerInputActions.Player.Jump.Disable();
        _playerInputActions.Player.Crouch.Disable();
        _playerInputActions.Player.Run.Disable();
    }

    private void InitializeState(PlayerState startingPlayerState)
    {
        _currentState = startingPlayerState;
        _currentState.Enter();
    }

    public void ChangeState(PlayerState newPlayerState)
    {
        _currentState.Exit();

        _previousState = _currentState;
        _currentState = newPlayerState;

        _currentState.Enter();
    }
}
